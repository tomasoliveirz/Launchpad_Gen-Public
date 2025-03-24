using Moongy.RD.Launchpad.Generator.Contracts.Core.Interfaces;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;

namespace Moongy.RD.Launchpad.SmartContractGenerator.Models;
public class GenerationResult<TToken> where TToken : IToken
{
    public string? Result { get; set; }
    public string? Error { get; set; }
    public TToken? TokenModel { get; set; }
    public List<ITokenomic> Tokenomics { get; set; } = [];
}
