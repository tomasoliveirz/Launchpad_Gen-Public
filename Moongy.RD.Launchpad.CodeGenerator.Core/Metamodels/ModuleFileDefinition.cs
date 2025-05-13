using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Directives;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Imports;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Interfaces;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Modules;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;
public class ModuleFileDefinition
{
    public List<DirectiveDefinition> Directives { get; set; } = [];
    public List<ImportDefinition> Imports { get; set; } = [];
    public List<EnumDefinition> Enums { get; set; } = [];
    public List<StructDefinition> Structs { get; set; } = [];
    public List<InterfaceDefinition> Interfaces { get; set; } = [];
    public List<ModuleDefinition> Modules { get; set; } = [];
}
