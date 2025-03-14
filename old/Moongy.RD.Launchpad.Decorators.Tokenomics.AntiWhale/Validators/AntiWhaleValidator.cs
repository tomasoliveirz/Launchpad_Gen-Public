using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.Decorators.Tokenomics.AntiWhale.Interfaces;
using Moongy.RD.Launchpad.Decorators.Tokenomics.AntiWhale.Models;

namespace Moongy.RD.Launchpad.Decorators.Tokenomics.AntiWhale.Validators
{
    public class AntiWhaleValidator : IAntiWhaleValidator
    {
        public void Validate(AntiWhaleTokenomic tokenomic)
        {
            if (tokenomic == null) return;
            if (tokenomic.MaxPercentage < 0 || tokenomic.MaxPercentage > 100) throw new InvalidRangeException("Anti-whale", 0, 100);
        }
    }
}
