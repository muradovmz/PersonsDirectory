using Application.Core;
using AutoMapper;
using Domain;
using Domain.Intefaces;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Persons
{
    public class Edit
    {
        public class Command:IRequest<Result<Unit>>
        {
            public Person Person { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Person).SetValidator(new PersonValidator());
            }
        }
        public class Handler:IRequestHandler<Command, Result<Unit>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {


                var person = _unitOfWork.Person.Table.SingleOrDefault(x=>x.Id==request.Person.Id);

                if (person == null) return null;

                _mapper.Map(request.Person, person);

                _unitOfWork.Person.Update(person);

                var result = await _unitOfWork.Complete() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update the person");

                return Result<Unit>.Success(Unit.Value);
            }
        }

    }
}
