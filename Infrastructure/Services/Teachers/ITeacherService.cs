using Domain.Models.TeachersDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Teachers
{
    public interface ITeacherService
    {
        List<GetTeacherDto> AllTeachers();
    }
}
