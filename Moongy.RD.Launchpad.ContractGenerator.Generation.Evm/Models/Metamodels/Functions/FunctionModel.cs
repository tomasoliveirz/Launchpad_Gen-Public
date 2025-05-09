using System.Collections.Generic;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Base;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Functions.Body;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Modifiers;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Parameters;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Functions
{
    public class FunctionModel : SolidityModel
    {
        public string? Name { get; set; }
        public SolidityFunctionTypeEnum FunctionType { get; set; } = SolidityFunctionTypeEnum.Normal;
        public List<FunctionParameterModel> Parameters { get; set; } = new();
        public List<ReturnParameterModel> ReturnParameters { get; set; } = new();
        public SolidityVisibilityEnum Visibility { get; set; } = SolidityVisibilityEnum.Public;
        public SolidityFunctionMutabilityEnum Mutability { get; set; } = SolidityFunctionMutabilityEnum.None;
        public List<ModifierModel> Modifiers { get; set; } = new();
        public FunctionBodyModel? Body { get; set; }
        
        public bool IsVirtual { get; set; }
        public bool IsOverride { get; set; }
        public bool IsInterfaceDeclaration { get; set; }
        public string? CustomError { get; set; }
        public List<string> OverrideSpecifiers { get; set; } = new();
    }
}