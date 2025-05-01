using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Parameters;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.TypeReferences;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Renderers.ComplexExpressions;

public class SolidityParameterSyntaxRenderer : BaseSoliditySyntaxRenderer<ParameterModel>
{
    public override string Render(ParameterModel model)
    {
        var typeText = new SolidityReferenceTypeSyntaxRenderer().Render(model.Type);
        var parts = new[] { typeText, GetMemoryLocation(model), GetIndexed(model), model.Name }
                    .Where(s => !string.IsNullOrEmpty(s));
        return string.Join(" ", parts);
    }

    public string RenderAsValue(ParameterModel model) 
    {
        if (model.Value is null)
            throw new ArgumentException(
                $"No value provided for parameter '{model.Name}'", nameof(model));

        // String values need quotes
        if (model.Type is SimpleTypeReference simple && simple.BaseType == SolidityDataTypeEnum.String)
            return $"\"{model.Value}\"";

        // Boolean values lowercased
        if (model.Type is SimpleTypeReference simpleBool && simpleBool.BaseType == SolidityDataTypeEnum.Bool)
            return model.Value.ToLowerInvariant();

        // Addresses as-is
        if (model.Type is SimpleTypeReference addr && addr.BaseType == SolidityDataTypeEnum.Address)
            return model.Value;

        // All others as-is
        return model.Value;
    }

    public string Render(IEnumerable<ParameterModel> model)
    {
        return string.Join(", ", model
                .OrderBy(m => m.Index)
                .Select(Render));
    }

    public string RenderAsValue(IEnumerable<ParameterModel> model)
    {
        return string.Join(", ", model
                .OrderBy(m => m.Index)
                .Select(RenderAsValue));
    }

    private static string GetMemoryLocation(ParameterModel model)
    {
        if (model is FunctionParameterModel fn && fn.Location.HasValue)
            return fn.Location.Value.ToString().ToLowerInvariant();
        if (model is ModifierParameterModel mod && mod.Location.HasValue)
            return mod.Location.Value.ToString().ToLowerInvariant();
        return string.Empty;
    }

    private static string GetIndexed(ParameterModel model)
    {
        if (model is EventParameterModel ev && ev.IsIndexed)
            return "indexed";
        return string.Empty;
    }

}
