using Moongy.RD.Launchpad.Core.Enums;

namespace Moongy.RD.Launchpad.Tools.TokenWizard.Models;
public class TokenWizardResponse
{
    public string? Token { get; set; }
    public string? Question { get; set; }
    public IEnumerable<TokenClassification> Tags { get; set; } = [];
    public IEnumerable<string> PossibleAnswers { get; set; } = [];
    public IEnumerable<string> PreviousResponses { get; set; } = [];
}
