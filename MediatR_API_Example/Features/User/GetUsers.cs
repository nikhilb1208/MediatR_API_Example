using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using MediatR_API_Example.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR_API_Example.Features.User
{
    public static class GetUsers
    {
        public class Query : IRequest<IReadOnlyList<User>>
        { }

        public class QueryValidator : AbstractValidator<Query>
        { }

        public class Handler : IRequestHandler<Query, IReadOnlyList<User>>
        {
            private readonly IExampleDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IExampleDataContext context, IMapper mapper)
            {
                this._context = context;
                this._mapper = mapper;
            }

            public async Task<IReadOnlyList<User>> Handle(Query request, CancellationToken cancellationToken)
            {
                var users = await _context.User
                                            .AsNoTracking()
                                            .ProjectTo<User>(_mapper.ConfigurationProvider)
                                            .ToListAsync(cancellationToken);
                return users;
            }
        }
    }
}
