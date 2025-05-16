using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Helpers
{
    public class IfRevertHelper
    {
        private readonly ExpressionDefinition _condition;
        private readonly string _errorName;
        private readonly List<ParameterDefinition> _revertParameters;

        public IfRevertHelper(ExpressionDefinition condition, string errorName, List<ParameterDefinition> revertParameters)
        {
            _condition = condition;
            _errorName = errorName;
            _revertParameters = revertParameters;
        }

        public FunctionStatementDefinition Build()
        {
            var errorTrigger = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Trigger,
                Trigger = new TriggerDefinition
                {
                    Kind = TriggerKind.Error,
                    Name = _errorName,
                    Parameters = _revertParameters,
                }
            };

            return new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Condition,
                ConditionBranches = new List<ConditionBranch>
        {
            new ConditionBranch
            {
                Condition = _condition,
                Body = new List<FunctionStatementDefinition> { errorTrigger }
            }
        }
            };
        }

    }

}
