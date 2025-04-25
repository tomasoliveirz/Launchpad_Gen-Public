using System;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces;
using Scriban;
using Scriban.Runtime;

namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Templates
{
    /// <summary>
    /// A lightweight wrapper that compiles a Scriban template once
    /// and renders it against a strongly-typed model.
    /// The entire model is exposed to the template via the global "Model" variable.
    /// </summary>
    /// <typeparam name="TModel">
    /// The CLR type of the model available inside the template.
    /// </typeparam>
    public class ScribanTemplateBase<TModel> : ITemplate<TModel>
    {
        private readonly Template _template;

        /// <summary>
        /// Initializes a new instance by parsing the provided template content.
        /// Throws <see cref="InvalidOperationException"/> if the template contains errors.
        /// </summary>
        /// <param name="templateContent">The raw Scriban template text.</param>
        public ScribanTemplateBase(string templateContent)
        {
            if (templateContent is null)
                throw new ArgumentNullException(nameof(templateContent));

            _template = Template.Parse(templateContent);

            if (_template.HasErrors)
                throw new InvalidOperationException(
                    $"Scriban template has {_template.Messages.Count} error(s): {string.Join(", ", _template.Messages)}");
        }

        /// <summary>
        /// Renders the precompiled template using the specified model instance.
        /// </summary>
        /// <param name="model">The model object to bind to the template.</param>
        /// <returns>The rendered output as a string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="model"/> is null.</exception>
        public string Render(TModel model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));

            var context = new TemplateContext
            {
                // Preserve .NET member names (PascalCase) in the template
                MemberRenamer = member => member.Name
            };

            // Expose the model under the global variable "Model"
            var globals = new ScriptObject { { "Model", model } };
            context.PushGlobal(globals);

            return _template.Render(context);
        }
    }
}