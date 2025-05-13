using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Interfaces;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Modules;
public class ModuleDefinition
{
    public required string Name { get; set; }
    public bool IsAbstract { get; set; }
    public List<ModuleDefinition> BaseModules { get; set; } = [];
    public List<InterfaceDefinition> Implements { get; set; } = [];
    public List<StructDefinition> Structs { get; set; } = [];
    public List<EnumDefinition> Enums { get; set; } = [];
    public List<FieldDefinition> Fields { get; set; } = [];
    public List<FunctionDefinition> Functions { get; set; } = [];
    public List<TriggerDefinition> Triggers { get; set; } = [];
    public List<ModifierDefinition> Modifiers { get; set; } = [];
    public AccessControlDefinition? AccessControl { get; set; }
}
