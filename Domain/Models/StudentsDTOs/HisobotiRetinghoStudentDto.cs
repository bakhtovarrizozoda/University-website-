using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.StudentsDTOs
{
    public class HisobotiRetinghoStudentDto
    {
        public string FacultyName { get; set; }
        public int Course { get; set; }
        public string GroupKod { get; set; }
        public int Hamagi { get; set; }
        public int SuporidR1 { get; set; }
        public int ProtcentR1 { get; set; }
        public int SuporidR2 { get; set; }
        public int ProtcentR2 { get; set; }
        public int Imtohon { get; set; }
        public int ProtcentIMT { get; set; }
        public string FanName { get; set; }
        public string Omuzgor { get; set; }
    }
}
