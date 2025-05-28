using Moongy.RD.Launchpad.CodeGenerator.Extensions.Attributes;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Models;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Attributes;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Models;
using Moongy.RD.Launchpad.Data.Forms.Extensions;
using Moongy.RD.Launchpad.Data.Forms.Tokenomics;

namespace Moongy.RD.Launchpad.Data.Forms;

[Standard(Source = StandardEnum.FungibleToken)]
public class FungibleTokenForm
{
    [StandardProperty(Source = StandardEnum.FungibleToken, Name =(nameof(FungibleTokenModel.Name)))]
    public required  string Name { get; set; }

    [StandardProperty(Source = StandardEnum.FungibleToken, Name =(nameof(FungibleTokenModel.Symbol)))]
    public required string Symbol { get; set; }

    [StandardProperty(Source = StandardEnum.FungibleToken, Name =(nameof(FungibleTokenModel.Premint)))]
    public long Premint { get; set; }
    
    [StandardProperty(Source = StandardEnum.FungibleToken, Name =(nameof(FungibleTokenModel.Decimals)))]
    public byte Decimals { get; set; }

    public long Supply {  get; set; }

    #region Tokenomics
    public TaxTokenomic? Tax { get; set; }
    public DeflationTokenomic? Deflation { get; set; }
    public BuyBackTokenomic? BuyBack { get; set; }
    public ReflectionTokenomic? Reflection { get; set; }
    public LiquidityGenerationTokenomic? LiquidityGeneration { get; set; }
    #endregion

    #region Extensions
    public AccessControl? AccessControl { get; set; }
    
    [Extension(Source = ExtensionEnum.Pausable)]
    public bool IsPausable { get; set; }
    [Extension(Source = ExtensionEnum.Permission)]
    public bool HasPermission { get; set; }
    public TokenVoting? Voting { get; set; }

    [Extension(Source = ExtensionEnum.Mint)]
    public bool HasMinting { get; set; }
    [Extension(Source = ExtensionEnum.Burn)]
    public bool HasBurning { get; set; }

    [ExtensionProperty(Source = ExtensionEnum.AntiWhale, Name = nameof(AntiWhaleExtensionModel.CapInPercentage)]
    public decimal? AntiWhaleCap { get; set; }

    /// <summary>
    /// Address or Role
    /// </summary>
    [Extension(Source =ExtensionEnum.TokenRecovery)]
    public string? RecoveryResponsible { get; set; }
    #endregion

}












