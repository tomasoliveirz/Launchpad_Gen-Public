using Moongy.RD.Launchpad.CodeGenerator.Core.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Core.Helpers;
using Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using ModuleDefinition = Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Modules.ModuleDefinition;
using TypeReference = Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others.TypeReference;

namespace Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Augmenters;

public abstract class BaseTokenomicAugmenter<TModel> : IAugmenter<TModel>
{
    public virtual IReadOnlyCollection<FeatureKind> Requires { get; }
    public virtual IReadOnlyCollection<FeatureKind> Provides { get; }
    
    public abstract void Augment(ContextMetamodel context, TModel model);

    protected ModuleDefinition Main(ContextMetamodel context)
        => context.Modules.Single(); // we assume there is only one contract per file
    
    protected static FunctionDefinition UpdateFn(ModuleDefinition m) =>
        m.Functions.First(f => f.Name is "_update");
    
    protected static FunctionDefinition TransferFn(ModuleDefinition m) =>
        m.Functions.First(f => f.Name is "_transfer");
    
    protected static void OverrideFunction(ModuleDefinition mod, FunctionDefinition existing, FunctionDefinition replacement)
    {
        if (existing == null) return;
        mod.Functions.Remove(existing);
        mod.Functions.Add(replacement);
    }
    protected static void AddOnce<T>(ICollection<T> list, Func<T, bool> exists, Func<T> factory)
    {
        if (list.Any(exists)) return;
        list.Add(factory());
    }
    
    protected static TypeReference T(PrimitiveType p) => TypeRef.Primitive(p);

}