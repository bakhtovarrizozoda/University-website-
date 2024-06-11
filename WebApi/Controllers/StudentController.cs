using Domain.Models.StudentsDTOs;
using Infrastructure.Services.Students;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private IStudentService _student;
        public StudentController(IStudentService student)
        {
            _student = student;
        }

        [HttpGet("All-Students")]
        public List<GetStudentDto> AllStudents()
        {
            var result = _student.AllStudents();
            return result;
        }

        [HttpGet("GetStudentFull")]
        public List<GetStudentFulDto> GetStudentFull()
        {
            var result = _student.GetStudentFull();
            return result;
        }

        [HttpGet("Search_Student")]
        public IActionResult SearchStudent([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var student = new SearchStudentDto
            {
                FirstName = firstName,
                LastName = lastName
            };
            var result = _student.SearchStudent(student);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound("Student not found.");
            }
        }


        [HttpGet("hisoboti_Retingho_Student")]
        public List<HisobotiRetinghoStudentDto> hisobotiRetinghoStudent()
        {
            var result = _student.hisobotiRetinghoStudent();
            return result;
        }

        [HttpGet("AllRuyxatDarsStudent")]
        public List<RuyxatDarsStudentDto> AllRuyxatDarsStudent()
        {
            var result = _student.AllRuyxatDarsStudent();
            return result;
        }


        [HttpGet("Ruyxati_Darshoi_Student")]
        public IActionResult SearchRuyxatiDarshoiStudent([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var searchDto = new SearchRuyxatiDarsStudentDto
            {
                FirstName = firstName,
                LastName = lastName
            };

            var result = _student.SearchRuyxatiDarshoiStudent(searchDto);

            if (result != null && result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return NotFound("Students not found.");
            }
        }


        [HttpGet("RuyxatiQarzdorho")]
        public List<RuyxatiQarzdorhoDto> RuyxatiQarzdorho()
        {
            var result = _student.RuyxatiQarzdorho();
            return result;
        }


        
        [HttpGet("fanRuyxatQarzdor")]
        public ActionResult<List<FanRuyxatQarzdorDto>> fanRuyxatQarzdor([FromQuery] GetByIdFanRuyxatQarzdor id)
        {
            if (id == null || id.StudentId <= 0)
            {
                return BadRequest("Invalid StudentId.");
            }

            var result = _student.fanRuyxatQarzdor(id.StudentId);
            return Ok(result);
        }

    }
}
