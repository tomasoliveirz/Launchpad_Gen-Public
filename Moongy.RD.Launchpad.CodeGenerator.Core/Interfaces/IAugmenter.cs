using Moongy.RD.Launchpad.CodeGenerator.Core.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces
{
    public interface IAugmenter<T> 
    {
        // augment will return a new ContextMetamodel with the new extension added
        // model is the original model
        // complement is the new extension/tokenomic model to be added
        void Augment(ContextMetamodel context, T model);
        
        // types of features that each augmenter requires to be able to augment the model
        IReadOnlyCollection<FeatureKind> Requires { get; }
        
        // features that each augmenter provides to the model
        IReadOnlyCollection<FeatureKind> Provides { get; }
    }
}
