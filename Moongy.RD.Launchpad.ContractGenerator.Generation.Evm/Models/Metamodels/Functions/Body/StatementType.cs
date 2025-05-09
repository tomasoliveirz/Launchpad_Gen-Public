namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Functions.Body
{
    /// <summary>
    /// Enumerates the statement types supported in Solidity function bodies.
    /// </summary>
    public enum StatementType
    {
        Raw,
        Require,
        Revert,
        If,
        MethodCall,
        Assignment,
        Emit,
        Return,
        Comment
    }
}