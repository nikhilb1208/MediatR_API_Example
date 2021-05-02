using AutoMapper;
using FluentValidation;
using MediatR;
using MediatR_API_Example.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR_API_Example.Features.User
{
    public static class DeleteUser
    {
        public class Command : IRequest<bool>
        {
            public int Id { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Id)
                    .GreaterThan(0);
            }
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly IExampleDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IExampleDataContext context, IMapper mapper)
            {
                this._context = context;
                this._mapper = mapper;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var userRecord = await _context.User.FindAsync(request.Id);

                if (userRecord != null)
                    throw new Exception("No user record found to delete.");

                _context.User.Remove(userRecord);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;

                return success;
            }
        }
    }
}
