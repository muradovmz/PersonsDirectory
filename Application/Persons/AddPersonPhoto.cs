using Application.Core;
using Application.Interfaces;
using Application.Persons.DTOs;
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
    public class AddPersonPhoto
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
            public PersonPhotoDto PhotoDto { get; set; }
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

                if (person == null) return null;

                if (request.PhotoDto.Photo.Length > 0)
                {
                    var photo = await _photoService.SaveToDiskAsync(request.PhotoDto.Photo);

                    if (photo != null)
                    {
                        person.AddPhoto(photo.PictureUrl, photo.FileName);

                        _unitOfWork.Person.Update(person);

                        var result = await _unitOfWork.Complete() > 0;

                        return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Failed to add person's photo");
                    }
                    else
                    {
                        return Result<Unit>.Failure("Problem saving photo to disk");
                    }
                }
                else
                    return Result<Unit>.Failure("Problem saving photo to disk");
            }
        }
    }
}
