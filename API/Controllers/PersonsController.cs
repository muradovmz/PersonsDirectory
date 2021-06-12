using Application.Core;
using Application.Persons;
using Application.Persons.DTOs;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class PersonsController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Person>>> GetPersons([FromQuery] PersonParams param)
        {
            return HandlePagedResult(await Mediator.Send(new List.Query { Params = param }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson(Person person)
        {
            return HandleResult(await Mediator.Send(new Create.Command { Person = person }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditPerson(Guid id, Person person)
        {
            person.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { Person = person }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DelatePerson(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }

        [HttpPost("{id}/addRelatedPerson")]
        public async Task<IActionResult> AddRelatedPerson(Guid id, RelatedPerson relatedPerson)
        {
            return HandleResult(await Mediator.Send(new AddRelatedPerson.Command { Id = id, RelatedPerson = relatedPerson }));
        }

        [HttpPost("{id}/removeRelatedPerson")]
        public async Task<IActionResult> RemoveRelatedPerson(Guid id, Guid relatedPersonId)
        {
            return HandleResult(await Mediator.Send(new RemoveRelatedPerson.Command { PersonId = id, RelatedPersonId = relatedPersonId }));
        }

        [HttpPost("{id}/photo/{photoId}")]
        public async Task<IActionResult> SetMainPhoto(Guid id, int photoId)
        {
            return HandleResult(await Mediator.Send(new SetPersonMainPhoto.Command { Id = id, PhotoId = photoId }));
        }

        [HttpPut("{id}/photo")]
        public async Task<IActionResult> AddPersonPhoto(Guid id, [FromForm] PersonPhotoDto photoDto)
        {
            return HandleResult(await Mediator.Send(new AddPersonPhoto.Command { Id = id, PhotoDto = photoDto }));
        }

        [HttpDelete("{id}/photo/{photoId}")]
        public async Task<ActionResult> DeletePersonPhoto(Guid id, int photoId)
        {
            return HandleResult(await Mediator.Send(new RemovePersonPhoto.Command { Id = id, PhotoId = photoId }));
        }
    }
}
