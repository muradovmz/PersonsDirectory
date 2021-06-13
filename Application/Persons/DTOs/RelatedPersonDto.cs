using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Persons.DTOs
{
    public class RelatedPersonDto
    {
        public string PrivateNumber { get; set; }
        public string FirstnameGE { get; set; }
        public string LastnameGE { get; set; }
        public DateTime Birthdate { get; set; }
        public string Phone { get; set; }
        public string Relationship { get; set; }
    }
}
