using System.ComponentModel.DataAnnotations;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Validators.Functions
{
    public class FunctionValidator : ContextModelValidator<FunctionDefinition>
    {
        private FunctionSignatureValidator? _sigValidator;
        public override void Validate(FunctionDefinition f)
        {
            base.Validate(f);
            _sigValidator ??= new FunctionSignatureValidator();
            _sigValidator.Validate(f);

            // Additional checks: modifiers uniqueness
            var modDups = f.Modifiers
                .GroupBy(m => m.Name, StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();
            if (modDups.Count != 0)
                throw new ValidationException(
                    $"Duplicate modifiers on function '{f.Name}': {string.Join(", ", modDups)}");
        }
    }

}
