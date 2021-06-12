using Application.Persons.DTOs;
using AutoMapper;
using Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Core
{
    public class PersonUrlResolver : IValueResolver<Person, PersonDto, string>
    {
        private readonly IConfiguration _config;
        public PersonUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Person source, PersonDto destination, string destMember, ResolutionContext context)
        {
            var photo = source.Photos.FirstOrDefault(x => x.IsMain);

            if (photo != null)
            {
                return _config["ApiUrl"] + photo.PictureUrl;
            }

            return _config["ApiUrl"] + "images/people/person.png";
        }
    }
}
