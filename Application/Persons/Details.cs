using Application.Core;
using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Persons
{
    public class Details
    {
        public class Query:IRequest<Result<Person>>
        {
            public Guid Id { get; set; }
        }

        public class Handler:IRequestHandler<Query, Result<Person>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Person>> Handle(Query request, CancellationToken cancellationToken)
            {
                var person = await _context.Persons.FindAsync(request.Id);
                return Result<Person>.Success(person);
            }
        }
    }
}
