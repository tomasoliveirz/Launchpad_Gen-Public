using Moongy.RD.Launchpad.CodeGenerator.Core.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Core.Helpers;
using Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using ModuleDefinition = Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Modules.ModuleDefinition;
using TypeReference = Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others.TypeReference;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Augmenters;

public abstract class BaseExtensionAugmenter<TModel> : IAugmenter<TModel>
{
    public abstract void Augment(ContextMetamodel context, TModel model);
    
    protected static ModuleDefinition Main(ContextMetamodel context) => context.Modules.Single();
    
    protected static void AddOnce<T>(ICollection<T> list, Func<T, bool> exists, Func<T> factory)
    {
        if (list.Any(exists)) return;
        list.Add(factory());
    }
    
    // we can just erite T(p) which is more readable
    protected static TypeReference T(PrimitiveType p) => TypeRef.Primitive(p);
}