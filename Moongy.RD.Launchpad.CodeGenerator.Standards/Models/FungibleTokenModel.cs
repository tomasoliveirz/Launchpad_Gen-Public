using Moongy.RD.Launchpad.CodeGenerator.Core.Attributes;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Enums;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Models;
public class FungibleTokenModel:BaseContractModel
{
    [Required(Name="Symbol")]
    [ContextProperty(Name="_symbol", Type =PrimitiveType.String, Visibility =Visibility.Private, HasDefaultValue=true)]
    public string? Symbol { get; set; }

    [ContextProperty(Name= "_decimals", Type =PrimitiveType.Uint8, Visibility =Visibility.Private, HasDefaultValue =true)]
    public byte Decimals { get; set; } = 18;

    [ContextProperty(Name = "_max_supply", Type = PrimitiveType.Uint256, Visibility = Visibility.Private, HasDefaultValue = false)]
    public ulong MaxSupply { get; set; } = 0;

    public ulong Premint { get; set; } = 0;

    public SupplyType SupplyType { get; set; }
    
}
