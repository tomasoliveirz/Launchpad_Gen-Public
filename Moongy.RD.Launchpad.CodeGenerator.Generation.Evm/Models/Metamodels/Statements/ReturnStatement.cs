using System.Collections.Generic;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Statements
{
    public class ReturnStatement : StatementModel
    {
        public List<string> Values { get; set; } = new List<string>();
        public List<ExpressionModel> ValueExpressions { get; set; } = new List<ExpressionModel>();
        
        protected override string TemplateBaseName => "ReturnStatement";
        
        public ReturnStatement()
        {
        }
        
        public ReturnStatement(params string[] values)
        {
            if (values != null)
            {
                Values.AddRange(values);
            }
        }
        
        public ReturnStatement(params ExpressionModel[] expressions)
        {
            if (expressions != null)
            {
                foreach (var expr in expressions)
                {
                    ValueExpressions.Add(expr);
                    Values.Add(expr.ToString());
                }
            }
        }
        
        public ReturnStatement AddValue(string value)
        {
            Values.Add(value);
            return this;
        }
        
        public ReturnStatement AddValue(ExpressionModel valueExpression)
        {
            ValueExpressions.Add(valueExpression);
            Values.Add(valueExpression.ToString());
            return this;
        }
    }
}