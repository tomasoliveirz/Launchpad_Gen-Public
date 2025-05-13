using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Directives;
using Moongy.RD.Launchpad.Core.Validators;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Validators
{
    public class DirectiveValidator : LaunchpadValidator<DirectiveDefinition>
    {
        public override void Validate(DirectiveDefinition d)
        {
            base.Validate(d);
            if (d.Kind == DirectiveKind.Custom)
            {
                if (string.IsNullOrWhiteSpace(d.Value))
                    throw new ValidationException("Custom directive must have a non-empty value.");
            }
            else
            {
                // For Version and License directives, ensure non-empty
                if (string.IsNullOrWhiteSpace(d.Value))
                    throw new ValidationException($"Directive '{d.Kind}' requires a value.");
            }
        }
    }
}
