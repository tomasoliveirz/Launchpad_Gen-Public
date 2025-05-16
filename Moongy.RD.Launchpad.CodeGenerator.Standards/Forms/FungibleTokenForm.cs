using Moongy.RD.Launchpad.CodeGenerator.Standards.Attributes;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Models;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Attributes;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Models.Tax;


namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Forms
{
    public class FungibleTokenForm
    {
        [StandardProperty(Source = StandardEnum.FungibleToken, Name = nameof(FungibleTokenModel.Name))]
        public required String Name { get; set; }
        [StandardProperty(Source = StandardEnum.FungibleToken, Name = nameof(FungibleTokenModel.Symbol))]
        public required String Symbol { get; set; }
        [StandardProperty(Source = StandardEnum.FungibleToken, Name = nameof(FungibleTokenModel.Decimals))]
        public byte Decimals { get; set; }
        [StandardProperty(Source = StandardEnum.FungibleToken, Name = nameof(FungibleTokenModel.Premint))]
        public long Premint { get; set; }
        public long Supply { get; set; }
        public TaxTokenomic? Tax { get; set; }
    }

    [Tokenomic(Source = CodeGenerator.Tokenomics.Enums.TokenomicEnum.Tax)]
    public class TaxTokenomic
    {
        public double TaxFee { get; set; }
        public List<TaxRecipient> Recipients { get; set; } = [];
    }

    public class TaxRecipient
    {
        public required String Address { get; set; }
        public decimal Shares { get; set; }
    }
}
