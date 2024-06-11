using Domain.Models.GroupsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Groups
{
    public interface IGroupService
    {
        List<GetGroupDto> AllGroups();
        List<Get530102AKurs4Dto> Group530102AKurs4();
    }
}
