using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moongy.RD.Launchpad.Decorators.Tokenomics.AntiWhale.Models;

namespace Moongy.RD.Launchpad.Decorators.Tokenomics.AntiWhale.Interfaces
{
    public interface IAntiWhaleValidator : /*ITokenomicValidator<AntiWhaleTokenomic>*/
    {
        public void Validate(AntiWhaleTokenomic tokenomic);
    }
}
