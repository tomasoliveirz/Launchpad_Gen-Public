using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Exceptions;
using Scriban;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors;

public class BaseTemplateProcessor<TModel>
{
    protected readonly Template _template;
    protected BaseTemplateProcessor(string @namespace, string templateName, string extension)
    {
        var assembly = typeof(BaseSolidityTemplateProcessor<TModel>).Assembly;
        var fileName = $"{templateName}.{extension}";
        var path = $"{@namespace}.{fileName}";
        using var stream = assembly.GetManifestResourceStream(path) ?? throw new InvalidTemplateException(path);
        using var reader = new StreamReader(stream);
        var templateText = reader.ReadToEnd();
        _template = Template.Parse(templateText, fileName);
        if (_template.HasErrors) throw new ErrorParsingTemplateException(path, _template.Messages.ToString());
    }
    public virtual string Render(TModel model)
    {
        return _template.Render(model, member => member.Name);
    }
    protected virtual string Render(object model)
    {
        return _template.Render(model, member => member.Name);
    }
}

public class BaseSolidityTemplateProcessor<TModel> : BaseTemplateProcessor<TModel>
{
    public BaseSolidityTemplateProcessor(string templateName) : base("Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Templates", templateName, "solidity.scriban")
    {
    }
}