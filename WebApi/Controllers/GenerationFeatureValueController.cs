using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moongy.RD.Launchpad.Business.Exceptions;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.Launchpad.Data.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerationFeatureValueController(IGenerationFeatureValueBusinessObject bo) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenerationFeatureValue>>> ListAsync()
        {
            var result = await bo.ListAsync();
            if (result.IsSuccessful) return Ok(result);
            return Problem(result.Exception?.Message ?? "");
        }

        [HttpPost("new")]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] GenerationFeatureValue value)
        {
            var result = await bo.CreateAsync(value,
                value.FeatureOnContractFeatureGroup?.Uuid ?? Guid.Empty,
                value.ContractGenerationResult?.Uuid ?? Guid.Empty);
            if (result.IsSuccessful) StatusCode(201, result.Result);
            if (result.Exception is InvalidModelException ime) return BadRequest(ime.Message);
            return Problem(result.Exception?.Message ?? "");
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(Guid uuid)
        {
            var result = await bo.DeleteAsync(uuid);
            if (result.IsSuccessful) return Ok();
            if (result.Exception is NotFoundException nfe) return NotFound(nfe.Id);
            return Problem(result.Exception?.Message ?? "");
        }

        [HttpGet("{uuid}")]
        public async Task<ActionResult<GenerationFeatureValue>> GetAsync(Guid uuid)
        {
            var result = await bo.GetAsync(uuid);
            if (result.IsSuccessful) return Ok(result.Result);
            if (result.Exception is NotFoundException nfe) return NotFound(nfe.Id);
            return Problem(result.Exception?.Message ?? "");
        }

        [HttpPut("{uuid}")]
        public async Task<ActionResult> UpdateAsync(Guid uuid, GenerationFeatureValue value)
        {
            var result = await bo.UpdateAsync(uuid, value,
                value.FeatureOnContractFeatureGroup?.Uuid,
                value.ContractGenerationResult?.Uuid);
            if (result.IsSuccessful) return Ok();
            if (result.Exception is NotFoundException nfe) return NotFound(nfe.Id);
            if (result.Exception is InvalidModelException ime) return BadRequest(ime.Message);
            return Problem(result.Exception?.Message ?? "");
        }
    }
}
