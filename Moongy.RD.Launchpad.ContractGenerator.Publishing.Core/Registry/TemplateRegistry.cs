using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Registry
{
    /// <summary>
    /// Registry for retrieving strongly-typed templates by model type.
    /// Acts as a service locator for templates.
    /// </summary>
    public class TemplateRegistry
    {
        private readonly Dictionary<Type, object> _templates = new Dictionary<Type, object>();
        
        /// <summary>
        /// Registers a template for a specific model type.
        /// </summary>
        /// <typeparam name="TModel">The model type.</typeparam>
        /// <param name="template">The template implementation to register.</param>
        /// <exception cref="ArgumentNullException">Thrown when template is null.</exception>
        public void RegisterTemplate<TModel>(ITemplate<TModel> template)
        {
            if (template == null)
                throw new ArgumentNullException(nameof(template));
                
            _templates[typeof(TModel)] = template;
        }
        
        /// <summary>
        /// Gets a template for a specific model type.
        /// </summary>
        /// <typeparam name="TModel">The model type.</typeparam>
        /// <returns>The registered template for the model type.</returns>
        /// <exception cref="InvalidOperationException">Thrown when no template is registered for the model type.</exception>
        public ITemplate<TModel> GetTemplate<TModel>()
        {
            if (_templates.TryGetValue(typeof(TModel), out var template))
            {
                return (ITemplate<TModel>)template;
            }
            
            throw new InvalidOperationException($"No template registered for type {typeof(TModel).Name}");
        }
        
        /// <summary>
        /// Checks if a template is registered for a specific model type.
        /// </summary>
        /// <typeparam name="TModel">The model type.</typeparam>
        /// <returns>True if a template is registered; otherwise, false.</returns>
        public bool HasTemplate<TModel>()
        {
            return _templates.ContainsKey(typeof(TModel));
        }
        
        /// <summary>
        /// Removes a template registration for a specific model type.
        /// </summary>
        /// <typeparam name="TModel">The model type.</typeparam>
        /// <returns>True if the template was successfully removed; otherwise, false.</returns>
        public bool UnregisterTemplate<TModel>()
        {
            return _templates.Remove(typeof(TModel));
        }
        
        /// <summary>
        /// Clears all template registrations.
        /// </summary>
        public void Clear()
        {
            _templates.Clear();
        }
    }
}