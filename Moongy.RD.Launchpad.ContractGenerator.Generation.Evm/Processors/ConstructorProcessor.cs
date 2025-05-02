using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Helpers;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.TypeReferences;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.ScribanRenderingModels;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors;


//TODO: Validate duplicate arguments, assigns with different data types, arguments with value that are being used on the base constructors,  and data location.
//TODO: Template doesn't provide any advantages. too simple.
public class ConstructorProcessor() : BaseSolidityTemplateProcessor<SolidityContractModel>("ContractConstructor")
{
    public override string Render(SolidityContractModel model)
    {
        var renderModel = Transform(model);
        var parameters = string.Join(", ", renderModel.Arguments);
        var baseConstructors = renderModel.BaseConstructors.Count > 0 
            ? " : " + string.Join(", ", renderModel.BaseConstructors) 
            : "";
        
        return Render((object)new 
        { 
            parameters, 
            baseConstructors, 
            assignments = renderModel.Assignments 
        });
    }

    private static ConstructorRenderingModel Transform(SolidityContractModel model)
    {
        var arguments = TransformArguments(model);
        var baseConstructors = TransformBaseConstructors(model);
        var assignments = TransformAssignments(model);

        return new() 
        { 
            Arguments = arguments, 
            BaseConstructors = baseConstructors, 
            Assignments = assignments
        };
    }

    private static List<string> TransformAssignments(SolidityContractModel model)
    {
        var assignments = new List<string>();

        foreach (var param in model.ConstructorParameters)
        {
            if (string.IsNullOrEmpty(param.AssignedTo))
                continue;

            var stateProp = model.StateProperties.FirstOrDefault(sp => sp.Name == param.AssignedTo);
            if (stateProp == null)
                throw new ArgumentException($"Constructor parameter '{param.Name}' is assigned to non-existent state property '{param.AssignedTo}'.");

            assignments.Add($"{param.AssignedTo} = {param.Name};");
        }

        return assignments;
    }

    private static List<string> TransformBaseConstructors(SolidityContractModel model)
    {
        if (model.BaseContracts.Count == 0)
            return [];
        
        // hashset que serve para evitarmos duplicados
        var processedConstructors = new HashSet<string>();
        var result = new List<string>();
        
        foreach (var bc in model.BaseContracts)
        {
            if (processedConstructors.Contains(bc.Name))
                continue;
            
            processedConstructors.Add(bc.Name);
            
            var arguments = bc.ConstructorParameters
                .OrderBy(p => p.Index)
                .Select(p => FormatArgumentValue(p.Value ?? p.Name, p.Type))
                .ToList();
            
            var argumentsString = string.Join(", ", arguments);
            result.Add($"{bc.Name}({argumentsString})");
        }

        return result;
    }

    private static string FormatArgumentValue(string value, TypeReference type)
    {
        // adicionamos aspas em caso de string
        if (type is SimpleTypeReference simpleType && 
            simpleType.BaseType == SolidityDataTypeEnum.String)
        {
            return $"\"{value}\"";
        }
    
        return value;
    }
    private static List<string> TransformArguments(SolidityContractModel model)
    {
        if (model.ConstructorParameters.Count == 0)
            return [];

        return model.ConstructorParameters
            .OrderBy(p => p.Index)
            .Select(p => 
            {
                var typeString = SolidityReferenceTypeSyntaxHelper.RenderTypeReference(p.Type);
                var locationString = p.Location.HasValue ? $" {p.Location.Value.ToString().ToLowerInvariant()}" : "";
                return $"{typeString}{locationString} {p.Name}";
            })
            .ToList();
    }
}
