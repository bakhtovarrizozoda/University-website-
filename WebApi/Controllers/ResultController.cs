using Domain.Models.ResultDTOs;
using Infrastructure.Services.Result;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResultController : ControllerBase
    {
        private IResultService _result;
        public ResultController(IResultService result)
        {
            _result = result;
        }

        [HttpGet("All-Result")]
        public List<GetResultDto> AllResult()
        {
            var result = _result.AllResult();
            return result;
        }
    }
}
