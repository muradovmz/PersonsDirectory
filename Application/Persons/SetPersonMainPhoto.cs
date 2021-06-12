using Application.Core;
using Application.Interfaces;
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
    public class SetPersonMainPhoto
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
            public int PhotoId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IPhotoService _photoService;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var person = _unitOfWork.Person.Table.SingleOrDefault(x => x.Id == request.Id);

                if (person.Photos.All(x => x.Id != request.PhotoId)) return null;

                person.SetMainPhoto(request.PhotoId);

                _unitOfWork.Person.Update(person);

                var result = await _unitOfWork.Complete() > 0;

                return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Failed to remove person's photo");

            }
        }
    }
}