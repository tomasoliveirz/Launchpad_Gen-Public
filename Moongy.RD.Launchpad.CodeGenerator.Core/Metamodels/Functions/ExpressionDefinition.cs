namespace Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions
{
    public class ExpressionDefinition
    {
        public ExpressionKind Kind { get; set; }

        #region Literal
        public string? LiteralValue { get; set; }
        #endregion

        #region Identifier
        public string? Identifier { get; set; }
        #endregion

        #region Binary Operations
        public ExpressionDefinition? Left { get; set; }
        public BinaryOperator Operator { get; set; }
        public ExpressionDefinition? Right { get; set; }
        #endregion

        #region Function call
        public ExpressionDefinition? Callee { get; set; }
        public List<ExpressionDefinition>? Arguments { get; set; }
        #endregion


        #region Member access
        public ExpressionDefinition? Target { get; set; }
        public string? MemberName { get; set; }
        #endregion


        #region Index access
        public ExpressionDefinition? IndexCollection { get; set; }
        public ExpressionDefinition? Index { get; set; }
        #endregion
    }
}
