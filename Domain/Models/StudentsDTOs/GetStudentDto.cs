using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.StudentsDTOs
{
    public class GetStudentDto
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Course { get; set; }
        public string Address { get; set; }
        public string Millat { get; set; }
        public string NamudiTahsil { get; set; }
        public string Shuba { get; set; }
        public string BirthDay { get; set; }
        public int FacultyId { get; set; }
        public int GroupId { get; set; }
    }
}
