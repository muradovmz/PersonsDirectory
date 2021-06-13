using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public class PersonSeedModel
    {
        public string Id { get; set; }
        public string PrivateNumber { get; set; }
        public string FirstnameGE { get; set; }
        public string FirstnameEN { get; set; }
        public string LastnameGE { get; set; }
        public string LastnameEN { get; set; }
        public DateTime Birthdate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PictureUrl { get; set; }
    }
}
