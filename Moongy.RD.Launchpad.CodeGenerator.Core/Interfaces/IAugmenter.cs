using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces
{
    public interface IAugmenter<T> 
    {
        public ContextMetamodel Augment(T complement, ContextMetamodel model);
    }
}
