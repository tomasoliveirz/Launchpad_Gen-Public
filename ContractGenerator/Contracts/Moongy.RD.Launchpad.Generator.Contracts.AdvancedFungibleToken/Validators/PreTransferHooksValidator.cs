using System.ComponentModel.DataAnnotations;
using Moongy.RD.Launchpad.Core.Exceptions;
using Moongy.RD.Launchpad.Generator.Contracts.AdvancedFungibleToken.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.AdvancedFungibleToken.Validators
{
    public static class PreTransferHooksValidator
    {
        public static void Validate(AdvancedFungibleTokenModel token)
        {
            if (token.PreTransferHooks == null)
            {
                throw new ValidationException("PreTransferHooks collection cannot be null for advanced fungible token");
            }

            for (int i = 0; i < token.PreTransferHooks.Count; i++)
            {
                if (token.PreTransferHooks[i] == null)
                {
                    throw new ValidationException($"PreTransferHook at index {i} cannot be null");
                }
            }
        }
    }
}