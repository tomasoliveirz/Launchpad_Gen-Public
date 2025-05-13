using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;
using Moongy.RD.Launchpad.Core.Validators;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Validators
{
    public class StructValidator : LaunchpadValidator<StructDefinition>
    {
        public override void Validate(StructDefinition s)
        {
            base.Validate(s);
            var duplicates = s.Fields
                .GroupBy(f => f.Name, StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();
            if (duplicates.Any())
                throw new ValidationException(
                    $"Duplicate struct fields in '{s.Name}': {string.Join(", ", duplicates)}");
        }
    }
}
