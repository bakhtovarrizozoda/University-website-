using Domain.Models.GroupsDTOs;
using Infrastructure.Services.Groups;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupController : ControllerBase
    {
        private IGroupService _group;
        public GroupController(IGroupService group)
        {
            _group = group;
        }

        [HttpGet("All-Groups")]
        public List<GetGroupDto> AllGroups()
        {
            var result = _group.AllGroups();
            return result;
        }

        [HttpGet("530102-A_Kurs4")]
        public List<Get530102AKurs4Dto> Group530102AKurs4()
        {
            var result = _group.Group530102AKurs4();
            return result;
        }
    }
}
