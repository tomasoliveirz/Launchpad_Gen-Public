namespace Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;

public class ContextMetamodel : ModuleFileDefinition
{
    public ContextMetamodel() {}

    public ContextMetamodel(ModuleFileDefinition src)
    {
        Directives = src.Directives;
        Imports    = src.Imports;
        Enums      = src.Enums;
        Structs    = src.Structs;
        Interfaces = src.Interfaces;
        Modules    = src.Modules;
    }

    public static ContextMetamodel From(ModuleFileDefinition src) => new(src);
}