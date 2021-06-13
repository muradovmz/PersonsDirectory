using Application.Persons;
using Application.Persons.DTOs;
using AutoMapper;
using Domain;
using EventBus.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //CreateMap<Person, Person>()
                //.ForMember(d => d.Photos, opt => opt.Ignore())
                //.ForMember(d => d.RelatedPeople, opt => opt.Ignore());
            CreateMap<Person, PersonDto>()
                .ForMember(d => d.RelatedPersons, o => o.MapFrom(s => s.RelatedPeople))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(x=>x.Photos.FirstOrDefault(y=>y.IsMain).PictureUrl));
            CreateMap<RelatedPerson, RelatedPersonDto>()
                .ForMember(d => d.PrivateNumber, o => o.MapFrom(s => s.RelatPerson.PrivateNumber))
                .ForMember(d => d.FirstnameGE, o => o.MapFrom(s => s.RelatPerson.FirstnameGE))
                .ForMember(d => d.LastnameGE, o => o.MapFrom(s => s.RelatPerson.LastnameGE))
                .ForMember(d => d.Birthdate, o => o.MapFrom(s => s.RelatPerson.Birthdate))
                .ForMember(d => d.Phone, o => o.MapFrom(s => s.RelatPerson.Phone))
                .ForMember(d => d.Relationship, o => o.MapFrom(s => s.Relationship.Designation));
            CreateMap<Photo, PhotoToReturnDto>()
                .ForMember(d => d.PictureUrl, o => o.MapFrom<PhotoUrlResolver>());
            CreateMap<Person, PersonCreateEvent>();
        }
    }
}
