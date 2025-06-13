using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Augmenters.AccessControl.Ownable
{
    public class InternalTransferOwnershipFunction
    {
        public FunctionDefinition Build()
        {
            var parameters = new List<ParameterDefinition>
            {
                new ParameterDefinition { Name = "newOwner", Type = DataTypeReference.Address }
            };

            var oldOwnerDeclaration = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.LocalDeclaration,
                LocalParameter = new ParameterDefinition
                {
                    Name = "oldOwner",
                    Type = DataTypeReference.Address
                }
            };

            var oldOwnerAssignment = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Assignment,
                ParameterAssignment = new AssignmentDefinition
                {
                    Left = new ExpressionDefinition
                    {
                        Kind = ExpressionKind.Identifier,
                        Identifier = "oldOwner"
                    },
                    Right = new ExpressionDefinition
                    {
                        Kind = ExpressionKind.Identifier,
                        Identifier = "_owner"
                    }
                }
            };

            var ownerAssignment = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Assignment,
                ParameterAssignment = new AssignmentDefinition
                {
                    Left = new ExpressionDefinition
                    {
                        Kind = ExpressionKind.Identifier,
                        Identifier = "_owner"
                    },
                    Right = new ExpressionDefinition
                    {
                        Kind = ExpressionKind.Identifier,
                        Identifier = "newOwner"
                    }
                }
            };

            var eventStatement = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Trigger,
                Trigger = new TriggerDefinition
                {
                    Kind = TriggerKind.Log,
                    Name = "OwnershipTransferred",
                    Parameters = new List<ParameterDefinition>
                    {
                        new() { Name = "previousOwner", Type = DataTypeReference.Address },
                        new() { Name = "newOwner", Type = DataTypeReference.Address }
                    }
                },
                TriggerArguments = new List<ExpressionDefinition>
                {
                    new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "oldOwner" },
                    new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "newOwner" }
                }
            };

            return new FunctionDefinition
            {
                Name = "_transferOwnership",
                Kind = FunctionKind.Normal,
                Visibility = Visibility.Internal,
                Parameters = parameters,
                ReturnParameters = new List<ParameterDefinition>(),
                Body = new List<FunctionStatementDefinition>
                {
                    oldOwnerDeclaration,
                    oldOwnerAssignment,
                    ownerAssignment,
                    eventStatement
                }
            };
        }
    }
}