using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractTypesController(IContractTypeDataAccessObject dao) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractType>>> ListAsync()
        {
            var result = await dao.ListAsync();
            return Ok(result);
        }

        [HttpPost("new")]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody]ContractType type)
        {
            var result = await dao.CreateAsync(type);
            return StatusCode(201, result);
        }
    }
}
