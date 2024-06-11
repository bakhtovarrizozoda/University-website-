using Domain.Models.FacultiesDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Faculties
{
    public interface IFacultySevice
    {
        List<GetFacultyDto> AllFaculties();
        List<GetFacultyGroupDto> GetFacultyGroupStatistics();
    }
}
