using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Helpers;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.State;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.TypeReferences;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors;

public class StatePropertyProcessor() : BaseSolidityTemplateProcessor<StatePropertyModel>("StateProperty")
{
    public override string Render(StatePropertyModel model)
    {
        var renderModel = Transform(model);
        return base.Render(renderModel);
    }

    private static object Transform(StatePropertyModel model)
    {
        var typeString = SolidityReferenceTypeSyntaxHelper.RenderTypeReference(model.Type);
        
        // verificar se há valor inicial
        string initialValue = model.InitialValue;
        bool hasInitialValue = !string.IsNullOrEmpty(initialValue);
        
        // adicionar aspas se o tipo for string e o inicial value não estiver entre aspas
        if (hasInitialValue && model.Type is SimpleTypeReference simpleType && 
            simpleType.BaseType == SolidityDataTypeEnum.String && 
            !initialValue.StartsWith("\""))
        {
            initialValue = $"\"{initialValue}\"";
        }
        
        return new
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
