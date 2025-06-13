using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Helpers;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Augmenters.AccessControl.Ownable;

/*
     function transferOwnership(address newOwner) public virtual onlyOwner {
       if (newOwner == address(0)) {
           revert OwnableInvalidOwner(address(0));
       }
       _transferOwnership(newOwner);
   }
 */
public class TransferOwnershipFunction
{
    public FunctionDefinition Build()
    {
        var parameters = new List<ParameterDefinition>
        {
            new ParameterDefinition
            {
                Name = "newOwner",
                Type = DataTypeReference.Address
            }
        };

        var newOwnerExpr = new ExpressionDefinition
        {
            Kind = ExpressionKind.Identifier,
            Identifier = "newOwner"
        };

        var zeroAddress = new ExpressionDefinition
        {
            Kind = ExpressionKind.Identifier,
            Identifier = "address(0)"
        };

        var condition = new ExpressionDefinition
        {
            Kind = ExpressionKind.Binary,
            Operator = BinaryOperator.Equal,
            Left = newOwnerExpr,
            Right = zeroAddress
        };

        
        
        var errorHelper = new IfRevertHelper(condition, "OwnableInvalidOwner", new List<ExpressionDefinition> { zeroAddress});

        var transferCall = new ExpressionDefinition
        {
            Kind = ExpressionKind.FunctionCall,
            Callee = new ExpressionDefinition { Identifier = "_transferOwnership" },
            Arguments = new List<ExpressionDefinition> { newOwnerExpr }
        };

        var transferStatement = new FunctionStatementDefinition
        {
            Kind = FunctionStatementKind.Expression,
            Expression = transferCall
        };

        var result = new FunctionDefinition
        {
            Name = "transferOwnership",
            Visibility = Visibility.Public,
            Modifiers = new List<ModifierDefinition>
            {
                new ModifierDefinition { Name = "onlyOwner" }
            },
            Parameters = parameters,
            Body = new List<FunctionStatementDefinition>
            {
                errorHelper.Build(),
                transferStatement
            }
        };
        return result;
    }
    
}