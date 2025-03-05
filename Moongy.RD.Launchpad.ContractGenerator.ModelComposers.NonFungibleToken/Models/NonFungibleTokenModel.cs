using Moongy.RD.Launchpad.ModelComposers.ModelComposers.Core.Models;
using System.Text.Json.Nodes;
namespace Moongy.RD.Launchpad.ContractGenerator.ModelComposers.NonFungibleToken.Models;
public class NonFungibleTokenModel : TokenModel
{
    public string Name { get; set; }
    public bool IsAccessible { get; set; }
    public bool IsBurnable { get; set; }
    public bool IsMintable { get; set; }
    public bool IsPausable { get; set; }
    public bool HasPermission { get; set; }
    public bool IsUpgradable { get; set; }
    public string URI { get; set; }
    public string URIStorage { get; set; }
    public bool Voting { get; set; }

}
