using Moongy.RD.Launchpad.Core.Models.Metamodel.Base;
using Moongy.RD.Launchpad.Core.Models.Metamodel.Events;
using Moongy.RD.Launchpad.Core.Models.Metamodel.Executions;

namespace Moongy.RD.Launchpad.Core.Models.Metamodel;
public class SmartContractModel
{
    List<Import> Imports { get; set; } = [];
    public string? Name { get; set; }
    public List<Execution> Executions { get; set; } = [];  
    public List<ContractProperty> Properties { get; set; } = [];
    public List<ContractOperation> ConstructorOperations { get; set; } = []; 
    public List<SmartContractFunction> SmartContractFunctions { get; set; } = [];
}
