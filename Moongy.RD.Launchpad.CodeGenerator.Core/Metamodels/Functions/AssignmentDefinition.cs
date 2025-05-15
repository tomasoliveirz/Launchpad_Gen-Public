namespace Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions
{
    public class AssignmentDefinition
    {
        public ExpressionDefinition Left { get; set; } = default!;
        public ExpressionDefinition Right { get; set; } = default!;
    }
}
