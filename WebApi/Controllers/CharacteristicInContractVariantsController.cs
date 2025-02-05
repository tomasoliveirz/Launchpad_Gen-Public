using Microsoft.AspNetCore.Mvc;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.Launchpad.Data.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacteristicInContractVariantsController(ICharacteristicInContractVariantBusinessObject bo) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacteristicInContractVariant>>> ListAsync()
        {
            var result = await bo.ListAsync();
            return Ok(result);
        }

        [HttpPost("new")]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CharacteristicInContractVariant characteristicInContractVariant)
        {
            var result = await bo.CreateAsync(characteristicInContractVariant);
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
        public async Task<ActionResult<CharacteristicInContractVariant>> GetAsync(Guid uuid)
        {
            var result = await bo.GetAsync(uuid);
            return Ok(result);
        }
    }
}
