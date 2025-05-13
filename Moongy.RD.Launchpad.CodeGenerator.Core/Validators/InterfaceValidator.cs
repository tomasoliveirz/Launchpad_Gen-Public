using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Interfaces;
using Moongy.RD.Launchpad.Core.Validators;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Validators
{
    public class InterfaceValidator : LaunchpadValidator<InterfaceDefinition>
    {
        public FunctionSignatureValidator _functionSignatureValidator;


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
            if (dupNames.Any())
                throw new ValidationException(
                    $"Duplicate function signatures in interface '{iface.Name}': {string.Join(", ", dupNames)}");

            foreach (var sig in iface.Signatures)
            {
                var paramDups = sig.Parameters
                    .GroupBy(p => p.Name, StringComparer.OrdinalIgnoreCase)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();
                if (paramDups.Any())
                    throw new ValidationException(
                        $"Duplicate parameter names in signature '{sig.Name}': {string.Join(", ", paramDups)}");

                var returnDups = sig.ReturnParameters
                    .GroupBy(p => p.Name, StringComparer.OrdinalIgnoreCase)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();
                if (returnDups.Any())
                    throw new ValidationException(
                        $"Duplicate return parameter names in signature '{sig.Name}': {string.Join(", ", returnDups)}");
            }
        }
    }
}
