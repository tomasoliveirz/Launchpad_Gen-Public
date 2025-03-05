using Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Models;
using System.Text.Json.Nodes;
namespace Moongy.RD.Launchpad.ContractGenerator.ModelComposers.NonFungibleToken.Models;
public class NonFungibleTokenModel : TokenModel
{
    public string URI { get; set; }
    public string URIStorage { get; set; }

}
