using System.ComponentModel.DataAnnotations;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Validators.FileComponents
{
    public class EnumValidator : ContextModelValidator<EnumDefinition>
    {
        public override void Validate(EnumDefinition e)
        {
            base.Validate(e);
            var duplicates = e.Members
                .GroupBy(m => m, StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();
            if (duplicates.Count != 0)
                throw new ValidationException(
                    $"Duplicate enum members in '{e.Name}': {string.Join(", ", duplicates)}");
        }
    }
}
