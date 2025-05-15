namespace Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;

public class ConditionBranch
{
    public ExpressionDefinition? Condition { get; set; }
    public List<FunctionStatementDefinition> Body { get; set; } = [];
}
