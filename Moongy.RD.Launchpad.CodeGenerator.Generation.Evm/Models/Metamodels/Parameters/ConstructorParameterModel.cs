namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Parameters;
public class ConstructorParameterModel : FunctionParameterModel
{
    public string? AssignedTo { get; set; }
    public List<string> Statements { get; set; } = new();
}
