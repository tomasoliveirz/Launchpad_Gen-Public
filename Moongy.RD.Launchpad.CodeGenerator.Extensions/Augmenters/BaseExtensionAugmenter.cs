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

    public virtual IReadOnlyCollection<FeatureKind> Requires { get; }
    public virtual IReadOnlyCollection<FeatureKind> Provides { get; }

    protected static ModuleDefinition Main(ContextMetamodel context) => context.Modules.Single();
    
    protected static void AddOnce<T>(ICollection<T> list, Func<T, bool> exists, Func<T> factory)
    {
        if (list.Any(exists)) return;
        list.Add(factory());
    }
    
    protected static TypeReference T(PrimitiveType p) => TypeRef.Primitive(p);
}