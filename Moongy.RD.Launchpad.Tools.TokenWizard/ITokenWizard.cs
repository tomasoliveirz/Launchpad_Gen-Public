using Moongy.RD.Launchpad.Tools.TokenWizard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moongy.RD.Launchpad.Tools.TokenWizard
{
    public interface ITokenWizard
    {
        public TokenWizardResponse GetToken(TokenWizardRequest request);
    }
}
