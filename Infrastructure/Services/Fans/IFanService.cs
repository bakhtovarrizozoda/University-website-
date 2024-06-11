using Domain.Models.FansDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Fans
{
    public interface IFanService
    {
        List<GetFanDto> AllFans();
    }
}
