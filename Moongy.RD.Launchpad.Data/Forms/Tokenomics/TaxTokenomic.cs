using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Attributes;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Models.Tax;

namespace Moongy.RD.Launchpad.Data.Forms.Tokenomics
{
    [Tokenomic(Source = TokenomicEnum.Tax)]
    public class TaxTokenomic
    {
        [TokenomicProperty(Name = nameof(TaxTokenomicModel.TaxFee), Source = TokenomicEnum.Tax)]
        public double TaxFee { get; set; }
        public List<TaxRecipient> Recipients { get; set; } = [];
    }

    public class TaxRecipient
    {
        public required string Address { get; set; }
        public decimal Share { get; set; }
    }
}
