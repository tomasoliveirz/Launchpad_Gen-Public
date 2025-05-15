using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Interfaces;

public interface IStandardComposer<T>
{
    public ModuleFileDefinition Compose(T standard);
}
