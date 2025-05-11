using System;
using System.Linq;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Functions
{
    public class FreeFunctionModel : BaseFunctionModel
    {
        public required string Name { get; set; }
        public override string TemplateName => "FreeFunction";
        public override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentException("Free function must have a name");
            }
            
            // check for duplicate parameter names
            var parameterNames = Parameters.Select(p => p.Name).ToList();
            var duplicateNames = parameterNames
                .GroupBy(name => name)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();
                
            if (duplicateNames.Any())
            {
                throw new ArgumentException($"Duplicate parameter names: {string.Join(", ", duplicateNames)}");
            }
            if (Visibility != Evm.Enums.SolidityVisibilityEnum.Public)
            {
                throw new ArgumentException("Free functions do not support explicit visibility modifiers");
            }
            if (IsVirtual)
            {
                throw new ArgumentException("Free functions cannot be virtual");
            }
            if (IsOverride)
            {
                throw new ArgumentException("Free functions cannot override other functions");
            }
            if (Modifiers.Count > 0)
            {
                throw new ArgumentException("Free functions cannot have modifiers");
            }
        }
    }
}