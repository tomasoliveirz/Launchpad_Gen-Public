using Microsoft.AspNetCore.Mvc;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.Launchpad.Business.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractFeaturesController(IContractFeatureBusinessObject bo) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractFeature>>> ListAsync()
        {
            var result = await bo.ListAsync();
            return Ok(result);
        }

        [HttpPost("new")]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] ContractFeature contractFeature)
        {
            var result = await bo.CreateAsync(contractFeature);
            return StatusCode(201, result);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(Guid uuid)
        {
            var result = await bo.GetAsync(uuid) ?? throw new Exception("Result not found");
            await bo.DeleteAsync(result);
            return StatusCode(204, result);
        }

        [HttpGet("{uuid}")]
        public async Task<ActionResult<ContractFeature>> GetAsync(Guid uuid)
        {
            var result = await bo.GetAsync(uuid);
            return Ok(result);
        }

    }
}
