using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.TeachersDTOs
{
    public class GetTeacherDto
    {
        public int TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDay { get; set; }
        public string Vasifa { get; set; }
        public string Ixtisos { get; set; }
        public string Email { get; set; }
    }
}
