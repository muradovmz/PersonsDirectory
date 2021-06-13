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
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Persons
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<PersonDto>>> 
        {
            public PersonParams Params { get; set; }
        }

        public class Handler: IRequestHandler<Query, Result<PagedList<PersonDto>>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Result<PagedList<PersonDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _unitOfWork.Person.TableNoTracking
                    .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Params.PrivateNumber))
                {
                    query = query.Where(x => x.PrivateNumber == request.Params.PrivateNumber);
                }

                if (!string.IsNullOrWhiteSpace(request.Params.FirstnameGe))
                {
                    query = query.Where(x => x.FirstnameGE == request.Params.FirstnameGe);
                }
                if (!string.IsNullOrWhiteSpace(request.Params.LastnameGe))
                {
                    query = query.Where(x => x.LastnameGE == request.Params.LastnameGe);
                }

                return Result<PagedList<PersonDto>>.Success(
                    await PagedList<PersonDto>.CreateAsync(query, request.Params.PageNumber, request.Params.PageSize)
                    );
            }
        }
    }
}
