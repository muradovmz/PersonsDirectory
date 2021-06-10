using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Photo
    {
        public int Id { get; set; }
        public string PictureUrl { get; set; }
        public string FileName { get; set; }
        public bool IsMain { get; set; }
        public Person Person { get; set; }
        public Guid PersonId { get; set; }
    }
}
