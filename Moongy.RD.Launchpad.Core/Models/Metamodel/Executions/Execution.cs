using Moongy.RD.Launchpad.Core.Enums;

namespace Moongy.RD.Launchpad.Core.Models.Metamodel.Executions;
public class Execution
{
    public Execution(string name, ExecutionType type)
    {
        Name = name;
        Type = type;
    }

    public string Name { get; set; }
    public ExecutionType Type { get; set; }
    List<ExecutionArgument> Arguments { get; set; } = [];
}
