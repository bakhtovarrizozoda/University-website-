using Domain.Models.TeachersDTOs;
using Infrastructure.Services.Teachers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeacherController : ControllerBase
    {
        private ITeacherService _teacher;
        public TeacherController(ITeacherService teacher)
        {
            _teacher = teacher;
        }

        [HttpGet("All-Teachers")]
        public List<GetTeacherDto> AllTeachers()
        {
            var result = _teacher.AllTeachers();
            return result;
        }

    }
}
