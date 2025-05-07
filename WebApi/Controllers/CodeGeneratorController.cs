using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moongy.RD.Launchpad.Business.Exceptions;
using Moongy.RD.Launchpad.Business.Interfaces;
using Moongy.RD.Launchpad.Data.ContractModels;
using Moongy.RD.Launchpad.Data.Pocos;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeGeneratorController(ICodeGeneratorBusinessObject bo) : ControllerBase
    {

        private string TestCode = @"// SPDX-License-Identifier: MIT
// Compatible with OpenZeppelin Contracts ^5.0.0
pragma solidity ^0.8.22;

import {ERC20} from ""@openzeppelin/contracts/token/ERC20/ERC20.sol"";
import {ERC20Permit} from ""@openzeppelin/contracts/token/ERC20/extensions/ERC20Permit.sol"";

contract MyToken is ERC20, ERC20Permit {
    constructor() ERC20(""MyToken"", ""MTK"") ERC20Permit(""MyToken"") {}
}";

        #region SimpleToken
        [HttpPost("SimpleToken")]
        public ActionResult<CodeGenerationResult<FungibleTokenForm>> Generate([FromBody] FungibleTokenForm model)
        {
            //var result = await bo.Generate(model);
            var result = new CodeGenerationResult<FungibleTokenForm>() { Model = model, Code = TestCode };
            return Ok(result);
            //if (result.IsSuccessful) return StatusCode(201, result.Result);
            //if (result.Exception is InvalidModelException ime) return BadRequest(ime.Message);
            //return Problem(result.Exception?.Message ?? "");
        }

        #endregion

        public async Task<ActionResult> GenerateFungibleToken([FromBody]FungibleTokenForm model)
        {
            var result = await bo.Generate(model);
            return Ok(result);
        }

    }
}
