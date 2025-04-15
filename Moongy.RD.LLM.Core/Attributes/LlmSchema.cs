using Moongy.RD.LLM.Core.Enums;

namespace Moongy.RD.LLM.Core.Attributes;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class LlmSchemaAttribute : Attribute
{
    public LlmService Service { get; set; }
    public string? Value { get; set; }
}
