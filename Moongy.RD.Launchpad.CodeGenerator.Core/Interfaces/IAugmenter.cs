using Moongy.RD.Launchpad.CodeGenerator.Core.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces
{
    public interface IAugmenter<T> 
    {
        void Augment(ContextMetamodel context, T model);
    }
}
