namespace Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
public enum ExpressionKind
{
    Literal,
    Identifier,
    Binary,
    FunctionCall,
    MemberAccess,
    IndexAccess,
    Parenthesized
}
