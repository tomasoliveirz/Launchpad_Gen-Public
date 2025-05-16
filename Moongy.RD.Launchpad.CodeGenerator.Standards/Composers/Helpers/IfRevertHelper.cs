using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Helpers
{
    public class IfRevertHelper
    {
        private readonly ExpressionDefinition _condition;
        private readonly string _errorName;
        private readonly List<ExpressionDefinition> _errorArguments;

        public IfRevertHelper(ExpressionDefinition condition, string errorName, List<ExpressionDefinition> errorArguments)
        {
            _condition = condition;
            _errorName = errorName;
            _errorArguments = errorArguments;
        }

        public FunctionStatementDefinition Build()
        {
            var revertCall = new ExpressionDefinition
            {
                Kind = ExpressionKind.FunctionCall,
                Callee = new ExpressionDefinition { Identifier = _errorName },
                Arguments = _errorArguments
            };

            var revertStatement = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Revert,
                Expression = revertCall
            };

            return new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Condition,
                ConditionBranches = new List<ConditionBranch>
            {
                new ConditionBranch
                {
                    Condition = _condition,
                    Body = new List<FunctionStatementDefinition> { revertStatement }
                }
            }
            };
        }
    }

}
