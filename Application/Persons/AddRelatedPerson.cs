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
    public class AddRelatedPerson
    {
        public class Command:IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
            public RelatedPerson RelatedPerson { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.Id == request.RelatedPerson.RelatedPersonId) return null;
                else
                {
                    var person = _unitOfWork.Person.Table.SingleOrDefault(x => x.Id == request.Id);

                    if (person == null) return null;

                    var relPerson = new RelatedPerson
                    {
                        PersonId = request.Id,
                        RelatedPersonId = request.RelatedPerson.RelatedPersonId,
                        RelationshipId = request.RelatedPerson.RelationshipId
                    };

                    person.RelatedPeople.Add(relPerson);

                    var result = await _unitOfWork.Complete() > 0;

                    return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Failed to add related person");
                }

            }
        }
    }
}
