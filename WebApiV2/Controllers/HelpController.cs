using Microsoft.AspNetCore.Mvc;
using Moongy.RD.Launchpad.Tools.Aissistant.Enums;
using Moongy.RD.Launchpad.Tools.Aissistant.Interfaces;
using Moongy.RD.Launchpad.Tools.Aissistant.Models;

namespace WebApiV2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HelpController(IAissistant assistant) : ControllerBase
{
    

    [HttpPost("generate")]
    public async Task<ActionResult<string>> AutoGenerateSmartContract([FromBody]AiGenerationRequest request)
    {
        var aiRequest = new AissistantRequest() { 
            Operation = OperationType.Generate, 
            Description = request.Description,
            Version = request.Version,
            Language = request.Language,
        };
        var result = await assistant.Execute(aiRequest);
        return Ok(result);
    }

    [HttpPost("analyze")]
    public async Task<ActionResult<string>> AutoAnalyzeSmartContract([FromBody] AiCodeOperationRequest request)
    {
        var aiRequest = new AissistantRequest()
        {
            Operation = OperationType.Analyze,
            Description = request.Description,
            Code = request.Code,
        };
        var result = await assistant.Execute(aiRequest);
        return Ok(result);
    }


    [HttpPost("format")]
    public async Task<ActionResult<string>> AutoFormatSmartContract([FromBody] AiCodeOperationRequest request)
    {
        var aiRequest = new AissistantRequest()
        {
            Operation = OperationType.Format,
            Code = request.Code,
            Description = request.Description,
        };
        var result = await assistant.Execute(aiRequest);
        return Ok(result);
    }

    [HttpPost("optimize")]
    public async Task<ActionResult<string>> AutoOptimizeSmartContract([FromBody] AiCodeOperationRequest request)
    {
        var aiRequest = new AissistantRequest()
        {
            Operation = OperationType.Optimize,
            Code = request.Code,
            Description = request.Description,
        };
        var result = await assistant.Execute(aiRequest);
        return Ok(result);
    }
}

public class AiGenerationRequest
{
    public required string Description { get; set; }
    public string? Version { get; set; }
    public string? Language { get; set; }
}

public class AiCodeOperationRequest
{
    public string? Description { get; set; }
    public required string Code { get; set; }
}
