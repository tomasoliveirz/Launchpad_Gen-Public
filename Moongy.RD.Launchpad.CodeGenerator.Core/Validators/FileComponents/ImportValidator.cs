using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Imports;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Validators.FileComponents
{
    public class ImportValidator : ContextModelValidator<ImportDefinition>
    {
        private static readonly Regex IdentifierRegex = new(
            "^[A-Za-z_][A-Za-z0-9_]*$", RegexOptions.Compiled);

        public override void Validate(ImportDefinition i)
        {
            base.Validate(i);
            if (string.IsNullOrWhiteSpace(i.Path))
                throw new ValidationException("Import path must be provided.");

            if (!string.IsNullOrWhiteSpace(i.Name) && !IdentifierRegex.IsMatch(i.Name))
                throw new ValidationException($"Invalid import name '{i.Name}'");

            if (!string.IsNullOrWhiteSpace(i.Alias) && !IdentifierRegex.IsMatch(i.Alias))
                throw new ValidationException($"Invalid import alias '{i.Alias}'");

            if(!string.IsNullOrWhiteSpace(i.Alias) && !string.IsNullOrWhiteSpace(i.Name))
            {
                throw new ValidationException("Can't have an import with both name and alias");
            }
        }
    }
}
