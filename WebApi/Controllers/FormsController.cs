using Microsoft.AspNetCore.Mvc;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.Launchpad.Core.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormsController(IFormsBusinessObject formsBusinessObject) : ControllerBase
    {
        

        [HttpGet("votes")]
        public async Task<ActionResult<SelectOptions>> GetVotingOptions()
        {
            var result = await formsBusinessObject.GetVotingOptions();
            if (result.IsSuccessful) return Ok(result.Result);
            return Problem(result.Exception?.Message ?? "");
        }

        [HttpGet("access")]
        public async Task<ActionResult<SelectOptions>> GetAccessOptions()
        {
            var result = await formsBusinessObject.GetAccessOptions();
            if (result.IsSuccessful) return Ok(result.Result);
            return Problem(result.Exception?.Message ?? "");

        }

        [HttpGet("access")]
        public async Task<ActionResult<SelectOptions>> GetAccessOptions()
        {
            var result = await formsBusinessObject.GetAccessOptions();
            if (result.IsSuccessful) return Ok(result.Result);
            return Problem(result.Exception?.Message ?? "");

        }
    }
}
