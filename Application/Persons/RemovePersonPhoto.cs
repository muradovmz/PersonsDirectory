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
    public class RemovePersonPhoto
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

            public Handler(IUnitOfWork unitOfWork, IPhotoService photoService)
            {
                _unitOfWork = unitOfWork;
                _photoService = photoService;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var person = _unitOfWork.Person.Table.SingleOrDefault(x => x.Id == request.Id);

                var photo = person.Photos.SingleOrDefault(x => x.Id == request.PhotoId);

                if (photo != null)
                {
                    if (photo.IsMain)
                        return Result<Unit>.Failure("You cannot delete the main photo");

                    _photoService.DeleteFromDisk(photo);
                }
                else
                {
                    return Result<Unit>.Failure("Photo does not exist");
                }

                person.RemovePhoto(request.PhotoId);

                _unitOfWork.Person.Update(person);

                var result = await _unitOfWork.Complete() > 0;

                return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Failed to remove person's photo");

            }
        }

    }
}