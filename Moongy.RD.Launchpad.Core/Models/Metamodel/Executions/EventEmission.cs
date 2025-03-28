using Moongy.RD.Launchpad.Core.Enums;

namespace Moongy.RD.Launchpad.Core.Models.Metamodel.Executions
{
    public class EventEmission(string name) : Execution(name, ExecutionType.Emit)
    {

    }
}
