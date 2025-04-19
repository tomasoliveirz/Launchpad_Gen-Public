namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for a template that can render model data.
    /// </summary>
    /// <typeparam name="TModel">The type of model that can be rendered.</typeparam>
    public interface ITemplate<in TModel>
    {
        /// <summary>
        /// Renders the template using the provided model.
        /// </summary>
        /// <param name="model">The model containing data for rendering.</param>
        /// <returns>The rendered template as a string.</returns>
        string Render(TModel model);
    }
}