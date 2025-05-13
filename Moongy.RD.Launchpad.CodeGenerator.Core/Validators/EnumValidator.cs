using System.ComponentModel.DataAnnotations;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;
using Moongy.RD.Launchpad.Core.Validators;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Validators
{
    public class EnumValidator : LaunchpadValidator<EnumDefinition>
    {
        public override void Validate(EnumDefinition e)
        {
            base.Validate(e);
            var duplicates = e.Members
                .GroupBy(m => m, StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();
            if (duplicates.Any())
                throw new ValidationException(
                    $"Duplicate enum members in '{e.Name}': {string.Join(", ", duplicates)}");
        }
    }
}
