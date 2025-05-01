using Scriban;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Renderers.Templates
{
    public abstract class BaseTemplateRenderer<TModel>
    {
        protected readonly Template _template;
        protected BaseTemplateRenderer(string templateName)
        {
            var assembly = typeof(BaseTemplateRenderer<TModel>).Assembly;
            using var stream = assembly.GetManifestResourceStream($"Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Templates.{templateName}.solidity.scriban") ?? throw new InvalidOperationException($"Could not find embedded resource: {templateName}.solidity.scriban");
            using var reader = new StreamReader(stream);
            var templateText = reader.ReadToEnd();
            _template = Template.Parse(templateText, $"{templateName}.solidity.scriban");
            if (_template.HasErrors)
                throw new InvalidOperationException($"Error parsing header template: {_template.Messages}");
        }

        public virtual string Render(TModel model)
        {
            return _template.Render(model, member => member.Name);
        }

        protected string Render(object model)
        {
            return _template.Render(model, member => member.Name);
        }
    }
}
