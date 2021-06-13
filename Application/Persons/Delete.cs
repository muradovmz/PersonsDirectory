using Application.Core;
using Domain;
using Domain.Intefaces;
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
    public class Delete
    {
        public class Command:IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler:IRequestHandler<Command, Result<Unit>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var person = _unitOfWork.Person.Table.SingleOrDefault(x => x.Id == request.Id);

                if (person == null) return null;

                var relatedPersons = _unitOfWork.RelatedPerson.Table.Where(x => x.PersonId == request.Id || x.RelatedPersonId == request.Id);

                foreach (var item in relatedPersons)
                {
                    _unitOfWork.RelatedPerson.Delete(item);
                }

                _unitOfWork.Person.Delete(person);

                var result = await _unitOfWork.Complete() > 0;

                if (!result) return Result<Unit>.Failure("Failed to delete the person");

                return Result<Unit>.Success(Unit.Value);
            }
        }

    }
}
