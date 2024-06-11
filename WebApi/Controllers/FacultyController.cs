using Domain.Models.FacultiesDTOs;
using Infrastructure.Services.Faculties;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FacultyController : ControllerBase
    {
        private IFacultySevice _faculty;
        public FacultyController(IFacultySevice faculty)
        {
            _faculty = faculty;
        }

        [HttpGet("All-Faculties")]
        public List<GetFacultyDto> AllFaculties()
        {
            var result = _faculty.AllFaculties();
            return result;
        }

        [HttpGet("GetFacultyGroupStatistics")]
        public List<GetFacultyGroupDto> GetFacultyGroupStatistics()
        {
            var result = _faculty.GetFacultyGroupStatistics();
            return result;
        }
    }
}
