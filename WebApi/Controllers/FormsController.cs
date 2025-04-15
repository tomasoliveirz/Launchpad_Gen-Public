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

        [HttpGet("upgradeability")]
        public async Task<ActionResult<SelectOptions>> GetUpgradeabilityOptions()
        {
            var result = await formsBusinessObject.GetUpgradeabilityOptions();
            if (result.IsSuccessful) return Ok(result.Result);
            return Problem(result.Exception?.Message ?? "");
        }


        [HttpPost("TokenWizard")]
        public async Task<ActionResult<TokenWizardResponse>> GetToken(TokenWizardRequest request)
        {
            return Ok();
        }

        [HttpPost("TokenWeighter")]
        public async Task<ActionResult<TokenWeighterResponse>> GetTokenWeight(TokenWeighterRequest request)
        {
            return Ok();
        }
    }

    public class TokenWeighterRequest
    {
        public List<string> Features { get; set; }
    }


    //0-40 - dark green
    //41-60 - light green
    //61-70 - yellow
    //71-80 - dark yellow
    //81-90 - orange
    //91-... - red
    public class TokenWeighterResponse
    {
        public decimal TotalWeight { get; set; }
        public List<TokenWeighterItem> FeaturesWeight { get; set; } = [];
    }

    public class TokenWeighterItem
    {
        public string FeatureName { get; set; }
        public double Weight { get; set; }
    }

    public class TokenWizardResponse
    {
        public string Token { get; set; }
        public string Question { get; set; }
        public List<string> PossibleAnswers { get; set; }
        public List<string> PreviousResponses { get; set; }
    }

    public class TokenWizardRequest
    {
        public List<string> Responses { get; set; } = [];
    }
}
