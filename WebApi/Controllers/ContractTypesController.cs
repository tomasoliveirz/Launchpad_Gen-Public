using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moongy.RD.Launchpad.Data.Base;
using Moongy.RD.Launchpad.Data.Entities;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.Launchpad.Business.Exceptions;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractTypesController(IContractTypeBusinessObject bo) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractType>>> ListAsync()
        {
            var result = await bo.ListAsync();
            if (result.IsSuccessful) return Ok(result.Result);
            return Problem(result.Exception?.Message ?? "");
        }

        [HttpPost("new")]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] ContractType contractType)
        {
            var result = await bo.CreateAsync(contractType);
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
        public async Task<ActionResult<ContractType>> GetAsync(Guid uuid)
        {
            var result = await bo.GetAsync(uuid);
            if (result.IsSuccessful) return Ok(result.Result);
            if (result.Exception is NotFoundException nfe) return NotFound(nfe.Id);
            return Problem(result.Exception?.Message ?? "");
        }

        [HttpPut("{uuid}")]
        public async Task<ActionResult> UpdateAsync(Guid uuid, ContractType contractType)
        {
            var result = await bo.UpdateAsync(uuid, contractType);
            if (result.IsSuccessful) return Ok();
            if (result.Exception is NotFoundException nfe) return NotFound(nfe.Id);
            if (result.Exception is InvalidModelException ime) return BadRequest(ime.Message);
            return Problem(result.Exception?.Message ?? "");
        }
    }
}
