using Application.Core;
using Domain.Intefaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Persons
{
    public class RemoveRelatedPerson
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid PersonId { get; set; }
            public Guid RelatedPersonId { get; set; }
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
                var relatedPerson = _unitOfWork.RelatedPerson.Table.SingleOrDefault(x => x.PersonId == request.PersonId && x.RelatedPersonId == request.RelatedPersonId);

                if (relatedPerson == null) return null;

                _unitOfWork.RelatedPerson.Delete(relatedPerson);

                var result = await _unitOfWork.Complete() > 0;

                if (!result) return Result<Unit>.Failure("Failed to delete the related person");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}