namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.ScribanRenderingModels;

public class ConstructorRenderingModel
{
    public List<string> Assignments { get; set; } = [];
    public List<string> Arguments  { get; set; } = [];
    public List<string> BaseConstructors { get; set; } = [];
    public bool HasBaseConstructors => BaseConstructors != null;
}
