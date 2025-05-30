using System;
using System.Collections.Generic;
using System.Linq;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Helpers;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.ScribanRenderingModels;
using System.Reflection;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Helpers;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Functions;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.ScribanRenderingModels;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors
{
    public class FunctionProcessor : BaseSolidityTemplateProcessor<BaseFunctionModel>
    {
        private readonly SolidityParameterSyntaxHelper _parameterHelper;

        public FunctionProcessor() : base("Function")
        {
            _parameterHelper = new SolidityParameterSyntaxHelper();
        }

        public override string Render(BaseFunctionModel model)
        {
            try
            {
                model.Validate();

                var renderModel = PrepareRenderModel(model);

                // use the appropriate template for the function type
                if (model.TemplateName != "Function")
                {
                    var processor = new BaseSolidityTemplateProcessor<FunctionRenderingModel>(model.TemplateName);
                    return processor.Render(renderModel);
                }

                return Render((object)renderModel);
            }
            catch (Exception ex)
            {
                string functionName = GetFunctionName(model);
                return $"// Error rendering function {functionName}: {ex.Message}";
            }
        }

        private FunctionRenderingModel PrepareRenderModel(BaseFunctionModel model)
        {
            var renderModel = new FunctionRenderingModel
            {
                Name = GetFunctionName(model),
                Visibility = FormatVisibility(model.Visibility),
                Mutability = FormatMutability(model.Mutability),
                IsVirtual = model.IsVirtual,
                IsOverride = model.IsOverride,
                IsInterface = model.IsInterfaceDeclaration,
                CustomError = model.CustomError,
                HasCustomError = !string.IsNullOrEmpty(model.CustomError),
                OverrideSpecifier = FormatOverrideSpecifier(model.OverrideSpecifiers)
            };

            // Process parameters
            renderModel.HasParameters = model.Parameters.Count > 0;
            renderModel.Parameters = model.Parameters
                .OrderBy(p => p.Index)
                .Select(p => _parameterHelper.Render(p))
                .ToArray();

            // Process return parameters
            renderModel.HasReturnValues = model.ReturnParameters.Count > 0;
            renderModel.ReturnValues = model.ReturnParameters
                .OrderBy(p => p.Index)
                .Select(p => _parameterHelper.Render(p))
                .ToArray();

            // Process modifiers
            renderModel.HasModifiers = model.Modifiers.Count > 0;
            renderModel.Modifiers = model.Modifiers
                .Select(m => FormatModifier(m))
                .ToArray();

            // Process statements
            renderModel.HasStatements = model.Statements.Count > 0;
            renderModel.Statements = model.Statements
                .Select(s => SolidityStatementProcessor.Render(s))
                .ToList();

            return renderModel;
        }

        private string GetFunctionName(BaseFunctionModel model)
        {
            if (model is NormalFunctionModel normalFunction)
                return normalFunction.Name;
            else if (model is FreeFunctionModel freeFunction)
                return freeFunction.Name;
            return string.Empty;
        }

        private string FormatModifier(Models.Metamodels.Modifiers.ModifierModel modifier)
        {
            if (modifier.Arguments.Count == 0)
                return modifier.Name;

            return $"{modifier.Name}({string.Join(", ", modifier.Arguments)})";
        }

        private string FormatVisibility(SolidityVisibilityEnum visibility)
        {
            return visibility.ToString().ToLowerInvariant();
        }

        private string FormatMutability(SolidityFunctionMutabilityEnum mutability)
        {
            return mutability switch
            {
                SolidityFunctionMutabilityEnum.View => "view",
                SolidityFunctionMutabilityEnum.Pure => "pure",
                SolidityFunctionMutabilityEnum.Payable => "payable",
                _ => ""
            };
        }

        private string FormatOverrideSpecifier(List<string> specifiers)
        {
            if (specifiers == null || specifiers.Count == 0)
                return "";

            return $"({string.Join(", ", specifiers)})";
        }
    }
}