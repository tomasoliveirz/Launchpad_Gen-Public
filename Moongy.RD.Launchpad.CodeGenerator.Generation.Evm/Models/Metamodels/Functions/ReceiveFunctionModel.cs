using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Enums;
using System;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Functions
{
    public class ReceiveFunctionModel : BaseFunctionModel
    {
        public override string TemplateName => "Receive";
        
        public ReceiveFunctionModel()
        {
            // these must be external and payable
            Visibility = SolidityVisibilityEnum.External;
            Mutability = SolidityFunctionMutabilityEnum.Payable;
        }
        
        public override void Validate()
        {
            if (Visibility != SolidityVisibilityEnum.External)
            {
                throw new ArgumentException("Receive function must be external");
            }
            
            if (Mutability != SolidityFunctionMutabilityEnum.Payable)
            {
                throw new ArgumentException("Receive function must be payable");
            }
            
            if (Parameters.Count > 0)
            {
                throw new ArgumentException("Receive function must not have parameters");
            }
            
            if (ReturnParameters.Count > 0)
            {
                throw new ArgumentException("Receive function must not have return values");
            }
        }
    }
}