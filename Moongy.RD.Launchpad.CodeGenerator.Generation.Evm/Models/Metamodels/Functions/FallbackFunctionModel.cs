using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Helpers;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Functions
{
    public class FallbackFunctionModel : BaseFunctionModel
    {
        public override string TemplateName => "Fallback";
        
        public FallbackFunctionModel()
        {
            // these functions must be external
            Visibility = SolidityVisibilityEnum.External;
        }
        
        public override void Validate()
        {
            if (Visibility != SolidityVisibilityEnum.External)
            {
                throw new ArgumentException("Fallback function must be external");
            }
            
            if (Mutability == SolidityFunctionMutabilityEnum.View ||
                Mutability == SolidityFunctionMutabilityEnum.Pure)
            {
                throw new ArgumentException("Fallback function cannot be view or pure");
            }
            
            if (Parameters.Count > 1)
            {
                throw new ArgumentException("Fallback function can have at most one parameter");
            }
            
            // if it has a parameter, it must be bytes calldata
            if (Parameters.Count == 1)
            {
                var param = Parameters[0];
                var dataType = SolidityReferenceTypeSyntaxHelper.RenderTypeReference(param.Type);
                var location = param.Location ?? SolidityMemoryLocation.None;
                
                if (dataType != "bytes" || location != SolidityMemoryLocation.Calldata)
                {
                    throw new ArgumentException("Fallback function parameter must be 'bytes calldata'");
                }
                
                // with a parameter, it must return bytes memory
                if (ReturnParameters.Count != 1)
                {
                    throw new ArgumentException("Fallback function with parameter must return exactly one value");
                }
                
                var returnParam = ReturnParameters[0];
                var returnType = SolidityReferenceTypeSyntaxHelper.RenderTypeReference(returnParam.Type);
                var returnLocation = returnParam.Location ?? SolidityMemoryLocation.None;
                
                if (returnType != "bytes" || returnLocation != SolidityMemoryLocation.Memory)
                {
                    throw new ArgumentException("Fallback function with parameter must return 'bytes memory'");
                }
            }
            else if (ReturnParameters.Count > 0)
            {
                throw new ArgumentException("Fallback function without parameters cannot have return values");
            }
        }
        
        public FallbackFunctionModel MakePayable()
        {
            Mutability = SolidityFunctionMutabilityEnum.Payable;
            return this;
        }
    }
}