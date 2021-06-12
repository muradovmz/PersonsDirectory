using Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Persons
{
    public class PersonParams:PagingParams
    {
        public string PrivateNumber { get; set; }
        public string FirstnameGe { get; set; }
        public string LastnameGe { get; set; }
    }
}
