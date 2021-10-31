using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab3.Models
{
    public class StudentDto
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public List<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
}