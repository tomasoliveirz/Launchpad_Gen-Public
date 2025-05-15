
namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements.Expressions
{
    public class ExpressionStatementModel : StatementModel
    {
        public ExpressionModel Expression { get; set; }

        protected override string TemplateBaseName => "ExpressionStatement";

        public ExpressionStatementModel(ExpressionModel expression)
        {
            Expression = expression;
        }

        public override string ToString()
        {
            return Expression.ToString() + ";";
        }
    }

}
