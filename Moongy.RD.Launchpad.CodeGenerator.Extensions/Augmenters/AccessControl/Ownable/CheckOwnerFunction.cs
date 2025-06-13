using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Augmenters.AccessControl.Ownable
{
    public class CheckOwnerFunction
    {
        public FunctionDefinition Build()
        {
            //  _owner field directly instead of calling owner() function
            var ownerField = new ExpressionDefinition
            {
                Kind = ExpressionKind.Identifier,
                Identifier = "_owner"
            };

            var msgSender = new ExpressionDefinition
            {
                Kind = ExpressionKind.MemberAccess,
                Target = new ExpressionDefinition
                {
                    Kind = ExpressionKind.Identifier,
                    Identifier = "msg"
                },
                MemberName = "sender"
            };

            // if _owner != msg.sender
            var condition = new ExpressionDefinition
            {
                Kind = ExpressionKind.Binary,
                Left = ownerField,
                Operator = BinaryOperator.NotEqual,
                Right = msgSender
            };

            var revertStatement = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Trigger,
                Trigger = new TriggerDefinition
                {
                    Kind = TriggerKind.Error,
                    Name = "OwnableUnauthorizedAccount",
                    Parameters = new List<ParameterDefinition>
                    {
                        new() { Name = "account", Type = DataTypeReference.Address }
                    }
                },
                TriggerArguments = new List<ExpressionDefinition>
                {
                    msgSender
                }
            };

            var ifStatement = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Condition,
                ConditionBranches = new List<ConditionBranch>
                {
                    new ConditionBranch
                    {
                        Condition = condition,
                        Body = new List<FunctionStatementDefinition>
                        {
                            revertStatement
                        }
                    }
                }
            };

            return new FunctionDefinition
            {
                Name = "_checkOwner",
                Kind = FunctionKind.Normal,
                Visibility = Visibility.Internal,
                Parameters = new List<ParameterDefinition>(),
                ReturnParameters = new List<ParameterDefinition>(),
                Body = new List<FunctionStatementDefinition>
                {
                    ifStatement
                }
            };
        }
    }
}