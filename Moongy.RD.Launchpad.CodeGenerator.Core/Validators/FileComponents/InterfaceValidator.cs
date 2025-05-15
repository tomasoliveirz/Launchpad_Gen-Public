using System.ComponentModel.DataAnnotations;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Interfaces;
using Moongy.RD.Launchpad.CodeGenerator.Core.Validators.Functions;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Validators.FileComponents
{
    public class InterfaceValidator : ContextModelValidator<InterfaceDefinition>
    {
        public FunctionSignatureValidator? _functionSignatureValidator;


        public override void Validate(InterfaceDefinition iface)
        {
            base.Validate(iface);
            if (string.IsNullOrWhiteSpace(iface.Name))
                throw new ValidationException("Interface must have a name.");

            var dupNames = iface.Signatures
                .GroupBy(sig => sig.Name, StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();
            if (dupNames.Count != 0)
                throw new ValidationException(
                    $"Duplicate function signatures in interface '{iface.Name}': {string.Join(", ", dupNames)}");

            foreach (var sig in iface.Signatures)
            {
                var paramDups = sig.Parameters
                    .GroupBy(p => p.Name, StringComparer.OrdinalIgnoreCase)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();
                if (paramDups.Count != 0)
                    throw new ValidationException(
                        $"Duplicate parameter names in signature '{sig.Name}': {string.Join(", ", paramDups)}");

                var returnDups = sig.ReturnParameters
                    .GroupBy(p => p.Name, StringComparer.OrdinalIgnoreCase)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();
                if (returnDups.Count != 0)
                    throw new ValidationException(
                        $"Duplicate return parameter names in signature '{sig.Name}': {string.Join(", ", returnDups)}");
            }
        }
    }
}
