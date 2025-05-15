using System.ComponentModel.DataAnnotations;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Validators.FileComponents
{
    public class StructValidator : ContextModelValidator<StructDefinition>
    {
        public override void Validate(StructDefinition s)
        {
            base.Validate(s);
            var duplicates = s.Fields
                .GroupBy(f => f.Name, StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();
            if (duplicates.Count != 0)
                throw new ValidationException(
                    $"Duplicate struct fields in '{s.Name}': {string.Join(", ", duplicates)}");
        }
    }
}
