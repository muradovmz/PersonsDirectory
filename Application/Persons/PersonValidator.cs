using Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Persons
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(x => x.PrivateNumber).NotEmpty();
            RuleFor(x => x.FirstnameGE).NotEmpty();
            RuleFor(x => x.FirstnameEN).NotEmpty();
            RuleFor(x => x.LastnameGE).NotEmpty();
            RuleFor(x => x.LastnameEN).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty();
        }
    }
}
