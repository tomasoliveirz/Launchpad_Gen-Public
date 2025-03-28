using Moongy.RD.Launchpad.Core.Models.Metamodel.Events;
using Moongy.RD.Launchpad.Core.Models.Metamodel.Executions;

namespace Moongy.RD.Launchpad.Core.Models.Metamodel;
public class SmartContractModel
{
    SoftwareLicense Licence { get; set; }
    SoftwareVersion? Version { get; set; }
    List<Import> Imports { get; set; } = [];
    public string? Name { get; set; }
    public List<Execution> Executions { get; set; } = [];  
    public List<Event> Events { get; set; } = [];
    public List<ContractOperation> ConstructorOperations { get; set; } = [];
}
