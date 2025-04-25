using Moongy.RD.Launchpad.Core.Attributes;
using Moongy.RD.Launchpad.Core.Models.Metamodel.Base;
using Moongy.RD.Launchpad.Core.Models.Metamodel.Executions;

namespace Moongy.RD.Launchpad.Core.Models.Metamodel;
public class SmartContractModel
{
    public List<Import> Imports { get; set; } = [];
    [SmartContractType(Target = typeof(SoliditySmartContractModel), PropertyName = nameof(SoliditySmartContractModel.ContractName))]
    [SmartContractType(Target = typeof(RustSmartContractModel), PropertyName = nameof(RustSmartContractModel.ProgramName))]
    public string? Name { get; set; }
    public List<Execution> Executions { get; set; } = [];  
    public List<ContractProperty> Properties { get; set; } = [];
    public List<ContractOperation> ConstructorOperations { get; set; } = []; 
    public List<SmartContractFunction> SmartContractFunctions { get; set; } = [];
}

