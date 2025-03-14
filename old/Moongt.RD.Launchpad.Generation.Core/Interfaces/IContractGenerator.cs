
namespace Moongt.RD.Launchpad.Generation.Core.Interfaces;

public interface IContractGenerator<TModel>
{
    GenerationResult Generate(TModel model, ITemplate? template);
    ValidationResult Validate(TModel model);

}
