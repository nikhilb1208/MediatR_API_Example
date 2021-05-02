using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using MediatR_API_Example.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR_API_Example.Features.User
{
    public static class AddUser
    {
        public class Command : IRequest<User>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int SexId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.FirstName)
                    .MaximumLength(25)
                    .NotEmpty();

                RuleFor(x => x.LastName)
                    .MaximumLength(25)
                    .NotEmpty();

                RuleFor(x => x.SexId)
                    .GreaterThan(0);
            }
        }

        public class Handler : IRequestHandler<Command, User>
        {
            private readonly IExampleDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IExampleDataContext context, IMapper mapper)
            {
                this._context = context;
                this._mapper = mapper;
            }

            public async Task<User> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = _mapper.Map<Domain.User>(request);

                return await CreateUser(request, user, cancellationToken);
            }

            private async Task<User> CreateUser(Command request, Domain.User user, CancellationToken cancellationToken)
            {
                await UserBusinessRules(request, cancellationToken);

                var duplicate = await _context.User.Where(x => x.FirstName == request.FirstName &&
                                                         x.LastName == request.LastName &&
                                                         x.SexId == request.SexId).FirstOrDefaultAsync(cancellationToken);

                if (duplicate != null)
                    throw new Exception("User Already Exists.");

                var newUser = await _context.User.AddAsync(user, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                var record = await _context.User
                                            .AsNoTracking()
                                            .Where(x => x.Id == newUser.Entity.Id)
                                            .ProjectTo<User>(_mapper.ConfigurationProvider)
                                            .FirstOrDefaultAsync(cancellationToken);

                return record;
            }

            private async Task UserBusinessRules(Command request, CancellationToken cancellationToken)
            {
                List<ValidationFailure> failures = new List<ValidationFailure>();

                var sexInfo = await GetSexInformation(cancellationToken);

                bool sexIdCheck = sexInfo.Any(x => x.Id == request.SexId);

                if (!sexIdCheck)
                    failures.Add(new ValidationFailure("SexId", "Invalid SexId."));

                //ToDo - Create Middleware to handle exceptions
                if (failures.Count > 0)
                {
                    //throw new ValidationFailure(failures);
                    throw new Exception("Something went wrong.");
                }
            }

            private async Task<List<Sex>> GetSexInformation(CancellationToken cancellationToken)
            {
                var sexInformation = await _context.Sex
                                                    .AsNoTracking()
                                                    .ProjectTo<Sex>(_mapper.ConfigurationProvider)
                                                    .ToListAsync(cancellationToken);

                return sexInformation;
            }
        }
    }
}
