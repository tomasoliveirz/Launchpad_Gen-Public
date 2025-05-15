namespace Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;

public interface ICodeGenerator<TLanguageMetamodel>
{
    public string Generate(TLanguageMetamodel model);
}
