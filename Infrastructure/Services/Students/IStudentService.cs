using Domain.Models.StudentsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Students
{
    public interface IStudentService
    {
        List<GetStudentDto> AllStudents();
        List<GetStudentFulDto> GetStudentFull();
        List<GetStudentFulDto> SearchStudent(SearchStudentDto student);
        List<HisobotiRetinghoStudentDto> hisobotiRetinghoStudent();
        List<RuyxatDarsStudentDto> AllRuyxatDarsStudent();
        List<RuyxatDarsStudentDto> SearchRuyxatiDarshoiStudent(SearchRuyxatiDarsStudentDto search);
        List<RuyxatiQarzdorhoDto> RuyxatiQarzdorho();
        //List<RuyxatiQarzdorhoDto> SearchRuyxatiQarzdorho(SearchRuyxatiDarsStudentDto search);
        List<FanRuyxatQarzdorDto> fanRuyxatQarzdor(int studentId);
    }
}
