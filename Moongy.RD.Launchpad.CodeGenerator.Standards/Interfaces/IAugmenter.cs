using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;

namespace Moongy.RD.Launchpad.CodeGenerator.Standards.Interfaces
{
    public interface IAugmenter<T> 
    {
        public ContextMetamodel Augment(T complement, ContextMetamodel model);
    }
}
