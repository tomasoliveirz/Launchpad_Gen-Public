using Moongy.RD.Launchpad.CodeGenerator.Core.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Interfaces;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
public class FunctionDefinition : FunctionSignature
{
    public FunctionKind Kind { get; set; }
    public Visibility Visibility { get; set; }
    public List<ModifierDefinition> Modifiers { get; set; } = [];
    public AccessControlDefinition? AccessControl { get; set; }
    public List<FunctionStatementDefinition> Body { get; set; } = [];
    public StateMutability Mutability { get; set; } = StateMutability.Mutable;
}
