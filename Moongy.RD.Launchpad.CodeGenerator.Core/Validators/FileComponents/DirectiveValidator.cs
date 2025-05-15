using System.ComponentModel.DataAnnotations;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Directives;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Validators.FileComponents
{
    public class DirectiveValidator : ContextModelValidator<DirectiveDefinition>
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
