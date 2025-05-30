using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Helpers;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.State;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.TypeReferences;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.ScribanRenderingModels;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Helpers;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.State;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.TypeReferences;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.ScribanRenderingModels;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors;

public class StatePropertyProcessor() : BaseSolidityTemplateProcessor<StatePropertyModel>("StateProperty")
{
    public override string Render(StatePropertyModel model)
    {
        var renderModel = Transform(model);
        return base.Render(renderModel);
    }

    private static StatePropertyRenderingModel Transform(StatePropertyModel model)
    {
        var typeString = SolidityReferenceTypeSyntaxHelper.RenderTypeReference(model.Type);

        string initialValue = model.InitialValue;
        bool hasInitialValue = !string.IsNullOrEmpty(initialValue);

        if (hasInitialValue && model.Type is SimpleTypeReference simpleType &&
            simpleType.BaseType == SolidityDataTypeEnum.String &&
            !initialValue.StartsWith("\""))
        {
            initialValue = $"\"{initialValue}\"";
        }

        return new StatePropertyRenderingModel
        {
            Type = typeString,
            IsConstant = model.IsConstant,
            IsImmutable = model.IsImmutable,
            Visibility = GetVisibilityKeyword(model.Visibility),
            Name = model.Name,
            HasInitialValue = hasInitialValue,
            InitialValue = initialValue
        };
    }
    private static string GetVisibilityKeyword(SolidityVisibilityEnum visibility)
    {
        return visibility switch
        {
            SolidityVisibilityEnum.Public => "public",
            SolidityVisibilityEnum.Private => "private",
            SolidityVisibilityEnum.Internal => "internal",
            _ => "private"
        };
    }
}