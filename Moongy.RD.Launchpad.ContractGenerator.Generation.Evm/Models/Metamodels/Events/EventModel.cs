using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Base;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Parameters;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Events;
public class EventModel : SolidityModel
{
    public required string Name { get; set; }
    public List<EventParameterModel> Parameters { get; set; } = new();
}
