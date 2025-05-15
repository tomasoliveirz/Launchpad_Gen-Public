using System.ComponentModel.DataAnnotations;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Interfaces;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Validators.Functions
{
    public class FunctionSignatureValidator : ContextModelValidator<FunctionSignature>
    {
        public override void Validate(FunctionSignature sig)
        {
            base.Validate(sig);
            if (string.IsNullOrWhiteSpace(sig.Name))
                throw new ValidationException("Function signature must have a name.");

            var paramDups = sig.Parameters
                .GroupBy(p => p.Name, StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();
            if (paramDups.Count != 0)
                throw new ValidationException(
                    $"Duplicate parameter names in function '{sig.Name}': {string.Join(", ", paramDups)}");

            var returnDups = sig.ReturnParameters
                .GroupBy(p => p.Name, StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();
            if (returnDups.Count != 0)
                throw new ValidationException(
                    $"Duplicate return parameter names in function '{sig.Name}': {string.Join(", ", returnDups)}");

        }
    }
}
