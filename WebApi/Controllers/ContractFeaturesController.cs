using Microsoft.AspNetCore.Mvc;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractFeaturesController(IContractFeatureDataAccessObject dao) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractFeature>>> ListAsync()
        {
            var result = await dao.ListAsync();
            return Ok(result);
        }

        [HttpPost("new")]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] ContractFeature contractFeature)
        {
            var result = await dao.CreateAsync(contractFeature);
            return StatusCode(201, result);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(Guid uuid)
        {
            var result = await dao.GetAsync(uuid);
            if (result == null) throw new Exception("Result not found");
            await dao.DeleteAsync(result);
            return StatusCode(204, result);
        }

        [HttpGet("{uuid}")]
        public async Task<ActionResult<ContractFeature>> GetAsync(Guid uuid)
        {
            var result = await dao.GetAsync(uuid);
            return Ok(result);
        }

    }
}
