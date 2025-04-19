using System;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces;
using Scriban;
using Scriban.Runtime;

namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Templates
{
    /// <summary>
    /// Wrapper que compila um template Scriban uma única vez
    /// e o renderiza contra um modelo fortemente tipado.
    /// O modelo inteiro fica disponível no template como {{ Model }}.
    /// </summary>
    /// <typeparam name="TModel">
    /// Tipo de modelo exposto ao template.
    /// </typeparam>
    public class ScribanTemplateBase<TModel> : ITemplate<TModel>
    {
        private readonly Template _template;

        /// <summary>
        /// Constrói a instância e faz o parse do conteúdo.
        /// Lança <see cref="InvalidOperationException"/> se o template tiver erros.
        /// </summary>
        public ScribanTemplateBase(string templateContent)
        {
            if (templateContent is null)
                throw new ArgumentNullException(nameof(templateContent));

            _template = Template.Parse(templateContent);

            if (_template.HasErrors)
                throw new InvalidOperationException(
                    $"Template has {_template.Messages.Count} error(s): " +
                    string.Join(", ", _template.Messages));
        }

        /// <inheritdoc />
        public string Render(TModel model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));

            var context = new TemplateContext
            {
                // Mantém os nomes exatamente como estão no CLR (PascalCase)
                MemberRenamer = member => member.Name
            };

            // Expor o modelo inteiro sob o nome global "Model"
            var globals = new ScriptObject { { "Model", model } };
            context.PushGlobal(globals);

            return _template.Render(context);
        }
    }
}