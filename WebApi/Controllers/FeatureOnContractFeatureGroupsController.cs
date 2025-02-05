using Microsoft.AspNetCore.Mvc;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.LaunchPad.DataAccess.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureOnContractFeatureGroupsController(IFeatureOnContractFeatureGroupDataAccessObject dao) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeatureOnContractFeatureGroup>>> ListAsync()
        {
            var result = await dao.ListAsync();
            return Ok(result);
        }

        [HttpPost("new")]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] FeatureOnContractFeatureGroup featureOnContractFeatureGroup)
        {
            var result = await dao.CreateAsync(featureOnContractFeatureGroup);
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
        public async Task<ActionResult<FeatureOnContractFeatureGroup>> GetAsync(Guid uuid)
        {
            var result = await dao.GetAsync(uuid);
            return Ok(result);
        }
    }
}
