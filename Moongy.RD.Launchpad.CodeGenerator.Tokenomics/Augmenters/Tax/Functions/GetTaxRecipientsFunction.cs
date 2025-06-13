using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;

namespace Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Augmenters.Tax.Functions;

public class GetTaxRecipientsFunction
{
    public FunctionDefinition Build()
    {
        var taxRecipientsExpr = new ExpressionDefinition
        {
            Kind = ExpressionKind.Identifier,
            Identifier = "_taxRecipients"
        };

        var taxSharesExpr = new ExpressionDefinition
        {
            Kind = ExpressionKind.Identifier,
            Identifier = "_taxShares"
        };

        var returnStatement = new FunctionStatementDefinition
        {
            Kind = FunctionStatementKind.Return,
            ReturnValues = new List<ExpressionDefinition> { taxRecipientsExpr, taxSharesExpr }
        };

        var result = new FunctionDefinition
        {
            Name = "getTaxRecipients",
            Visibility = Visibility.Public,
            ReturnParameters = new List<ParameterDefinition>
            {
                new ParameterDefinition
                {
                    Name = "recipients",
                    Type = new TypeReference
                    {
                        Kind = TypeReferenceKind.Array,
                        ElementType = new TypeReference { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Address }
                    }
                },
                new ParameterDefinition
                {
                    Name = "shares",
                    Type = new TypeReference
                    {
                        Kind = TypeReferenceKind.Array,
                        ElementType = new TypeReference { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Uint256 }
                    }
                }
            },
            Body = new List<FunctionStatementDefinition>
            {
                returnStatement
            }
        };

        return result;
    }
}