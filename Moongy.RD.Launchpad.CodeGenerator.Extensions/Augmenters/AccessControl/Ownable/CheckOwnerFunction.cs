using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Helpers;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Augmenters.AccessControl.Ownable;

/*
     function _checkOwner() internal view virtual {
       if (owner() != _msgSender()) {
           revert OwnableUnauthorizedAccount(_msgSender());
       }
   }
*/
public class CheckOwnerFunction
{
    public FunctionDefinition Build()
    {
        var ownerCall = new ExpressionDefinition
        {
            Kind = ExpressionKind.FunctionCall,
            Callee = new ExpressionDefinition { Identifier = "owner" },
            Arguments = []
        };

        var msgSenderCall = new ExpressionDefinition
        {
            Kind = ExpressionKind.FunctionCall,
            Callee = new ExpressionDefinition { Identifier = "msg.sender" },
            Arguments = []
        };

        var condition = new ExpressionDefinition
        {
            Kind = ExpressionKind.Binary,
            Operator = BinaryOperator.NotEqual,
            Left = ownerCall,
            Right = msgSenderCall
        };

        
        var errorHelper = new IfRevertHelper(condition, "OwnableUnauthorizedAccount", new List<ExpressionDefinition> { msgSenderCall });

        return new FunctionDefinition
        {
            Name = "_checkOwner",
            Visibility = Visibility.Internal,
            Body = [errorHelper.Build()]
        };
    }
}