using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;

namespace Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;
public interface ILanguageSpecificModelSynthesizer<TLanguageModel>
{
    public TLanguageModel Synthesize(ContextMetamodel file);
}
