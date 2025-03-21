
using System.Text.RegularExpressions;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Enumerables;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Interfaces;

namespace Moongy.RD.Launchpad.Generator.Contracts.Core.Validators
{
    public static class MintValidator
    {
        public static void Validate(IMintableToken token)
        {
     
            if (!token.IsMintable) return;
            AccessValidator.Validate(token.Access, true);
        }
    }
}
