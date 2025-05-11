using System;
using System.Linq;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Functions
{
    /// <summary>
    /// Represents a normal named function in Solidity.
    /// </summary>
    public class NormalFunctionModel : BaseFunctionModel
    {
        public required string Name { get; set; }
        
        public override string TemplateName => "Function";
        
        public override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentException("Normal function must have a name");
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
            
            // validate interface declaration specifics
            if (IsInterfaceDeclaration)
            {
                ValidateInterfaceFunction();
            }
            
            // validate external function specifics
            if (Visibility == SolidityVisibilityEnum.External)
            {
                ValidateExternalFunction();
            }
        }
        
        private void ValidateInterfaceFunction()
        {
            // interface functions must be external
            if (Visibility != SolidityVisibilityEnum.External)
            {
                throw new ArgumentException("Interface functions must be external");
            }
            
            // must not have a body
            if (Statements.Count > 0)
            {
                throw new ArgumentException("Interface functions must not have a body");
            }
            
            //  cant be override
            if (IsOverride)
            {
                throw new ArgumentException("Interface functions cannot be marked as override");
            }
        }
        
        private void ValidateExternalFunction()
        {
            // external functions cannot use storage for parameters
            foreach (var param in Parameters)
            {
                if (param.Location == SolidityMemoryLocation.Storage)
                {
                    throw new ArgumentException($"External function parameter '{param.Name}' cannot use 'storage' location");
                }
            }
            
            // cannot use storage for return values
            foreach (var returnParam in ReturnParameters)
            {
                if (returnParam.Location == SolidityMemoryLocation.Storage)
                {
                    var paramName = string.IsNullOrEmpty(returnParam.Name) ? "unnamed" : returnParam.Name;
                    throw new ArgumentException($"External function return value '{paramName}' cannot use 'storage' location");
                }
            }
        }
    }
}