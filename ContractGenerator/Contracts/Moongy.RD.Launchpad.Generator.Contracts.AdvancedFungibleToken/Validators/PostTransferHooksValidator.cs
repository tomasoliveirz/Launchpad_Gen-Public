using System.ComponentModel.DataAnnotations;
using Moongy.RD.Launchpad.Generator.Contracts.AdvancedFungibleToken.Models;

namespace Moongy.RD.Launchpad.Generator.Contracts.AdvancedFungibleToken.Validators
{
    public static class PostTransferHooksValidator
    {
        public static void Validate(AdvancedFungibleTokenModel token)
        {
            if (token.PostTransferHooks == null)
            {
                throw new ValidationException("PostTransferHooks collection cannot be null for advanced fungible token");
            }

            for (int i = 0; i < token.PostTransferHooks.Count; i++)
            {
                if (token.PostTransferHooks[i] == null)
                {
                    throw new ValidationException($"PostTransferHook at index {i} cannot be null");
                }
            }
        }
    }
}