using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.FacultiesDTOs
{
    public class GetFacultyGroupDto
    {
        public string FacultyName { get; set; }
        public string GroupKod { get; set; }
        public int Course { get; set; }
        public int Hamagi { get; set; }
        public int Shartnomavi { get; set; }    
        public int Budget { get; set; }
        public int Quota { get; set; }
        public string GroupName { get; set; }

    }
}
