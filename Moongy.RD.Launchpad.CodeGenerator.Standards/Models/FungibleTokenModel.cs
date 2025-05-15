using Moongy.RD.Launchpad.CodeGenerator.Core.Attributes;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Enums;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Models;
public class FungibleTokenModel:BaseContractModel
{
    [Required(Name="Symbol")]
    [ContextProperty(Name="symbol", Type =PrimitiveType.String, Visibility =Visibility.Public, HasDefaultValue=true)]
    public string? Symbol { get; set; }

    [ContextProperty(Name= "decimals", Type =PrimitiveType.String, Visibility =Visibility.Public, HasDefaultValue =true)]
    public byte Decimals { get; set; } = 18;

    [ContextProperty(Name = "max_supply", Type = PrimitiveType.Uint256, Visibility = Visibility.Public, HasDefaultValue = true)]
    public ulong MaxSupply { get; set; } = 0;

    public ulong Premint { get; set; } = 0;

    public SupplyType SupplyType { get; set; }
    
}
