using Moongy.RD.Launchpad.CodeGenerator.Core.Validators;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Models;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Enums;
using System;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Validators.Common;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Validators;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Validators
{
    public class NonFungibleTokenValidator : LaunchpadValidator<NonFungibleTokenModel>
    {
        public override void Validate(NonFungibleTokenModel token)
        {
            base.Validate(token);
            
            if (token.HasURI)
            {
                UriValidator.Validate(token.URI, true);
                UriStorageValidator.Validate(token);
            }
        }

    }
}