using Moongy.RD.Launchpad.Tools.Aissistant.Enums;

namespace Moongy.RD.Launchpad.Tools.Aissistant.Attributes;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class LlmSchemaAttribute : Attribute
{
    public LlmService Service { get; set; }
    public string? Value { get; set; }
    public OperationType[] BestFor { get; set; } = [];
}
