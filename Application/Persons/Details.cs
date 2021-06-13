using Application.Core;
using Application.Persons.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Domain.Intefaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        public class Query:IRequest<Result<PersonDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler:IRequestHandler<Query, Result<PersonDto>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Result<PersonDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var person = await _unitOfWork.Person.TableNoTracking
                    .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                

                return Result<PersonDto>.Success(person);
            }
        }
    }
}
