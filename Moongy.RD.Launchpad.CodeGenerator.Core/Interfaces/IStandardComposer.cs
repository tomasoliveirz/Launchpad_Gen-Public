using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;

public interface IStandardComposer<T>
{
    public ModuleFileDefinition Compose(T standard);
}
