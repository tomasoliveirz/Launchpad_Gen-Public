using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Helpers
{
    public class IfRevertHelper(ExpressionDefinition condition, string errorName, List<ExpressionDefinition> revertParameters)
    {
        private readonly ExpressionDefinition _condition = condition;
        private readonly string _errorName = errorName;
        private readonly List<ExpressionDefinition> _revertParameters = revertParameters;

        public FunctionStatementDefinition Build()
        {
            var errorTrigger = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Trigger,
                Trigger = new TriggerDefinition
                {
                    Kind = TriggerKind.Error,
                    Name = _errorName,
                },
                TriggerArguments = _revertParameters
                
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
