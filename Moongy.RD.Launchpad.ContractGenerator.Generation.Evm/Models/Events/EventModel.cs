using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Base;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Parameters;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Events;
public class EventModel : SolidityModel
{
    public required string Name { get; set; }
    public List<EventParameterModel> Parameters { get; set; } = new();
}
