using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elasticsearch.Index
{
    public class Person
    {
        public string PrivateNumber { get; set; }
        public string FirstnameGE { get; set; }
        public string FirstnameEN { get; set; }
        public string LastnameGE { get; set; }
        public string LastnameEN { get; set; }
        public string Birthdate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
