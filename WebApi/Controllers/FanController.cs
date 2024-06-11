using Domain.Models.FansDTOs;
using Infrastructure.Services.Fans;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FanController : ControllerBase
    {
        private IFanService _fan;
        public FanController(IFanService fan)
        {
            _fan = fan;
        }

        [HttpGet("All-Fans")]
        public List<GetFanDto> AllFans()
        {
            var result = _fan.AllFans();
            return result;
        }
    }
}
