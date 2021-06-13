using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Persons.DTOs
{
    public class PhotoToReturnDto
    {
        public int Id { get; set; }
        public string PictureUrl { get; set; }
        public string FileName { get; set; }
        public bool IsMain { get; set; }
    }
}
