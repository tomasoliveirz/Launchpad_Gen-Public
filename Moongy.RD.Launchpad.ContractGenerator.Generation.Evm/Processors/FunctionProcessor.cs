using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Helpers;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Functions;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Functions.Body;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Modifiers;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Parameters;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.TypeReferences;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.ScribanRenderingModels;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Processors
{
    /// <summary>
    /// Processor responsible for rendering Solidity functions using Scriban templates.
    /// Implements full validation according to Solidity language rules (>=0.8.0).
    /// </summary>
    public class FunctionProcessor : BaseSolidityTemplateProcessor<FunctionModel>
    {
        private readonly SolidityParameterSyntaxHelper _parameterHelper;
        private readonly StringBuilder _debugInfo;
        
        public FunctionProcessor() : base("Function")
        {
            _parameterHelper = new SolidityParameterSyntaxHelper();
            _debugInfo = new StringBuilder();
        }

        public override string Render(FunctionModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _debugInfo.Clear();
            _debugInfo.AppendLine($"// DEBUG: Rendering function '{model.Name}' of type '{model.FunctionType}'");

            try
            {
                ValidateModel(model);
                var renderModel = PrepareRenderModel(model);
                
                DebugRenderingModel(renderModel);
                
                string result;
                switch (model.FunctionType)
                {
                    case SolidityFunctionTypeEnum.Fallback:
                        result = RenderWithTemplate("Fallback", renderModel);
                        break;
                    case SolidityFunctionTypeEnum.Receive:
                        result = RenderWithTemplate("Receive", renderModel);
                        break;
                    default:
                        result = Render((object)renderModel);
                        break;
                }
                
                _debugInfo.AppendLine("// DEBUG: Function rendered successfully");
                
                return result;
            }
            catch (Exception ex)
            {
                _debugInfo.AppendLine($"// ERROR: {ex.Message}");
                _debugInfo.AppendLine($"// Stack trace: {ex.StackTrace}");
                
                return $"{_debugInfo}\n// Error rendering function {model.Name ?? model.FunctionType.ToString()}: {ex.Message}";
            }
        }

        /// <summary>
        /// Renders a function with a specific template.
        /// </summary>
        /// <param name="templateName">Name of the template to be used.</param>
        /// <param name="model">Function rendering model.</param>
        /// <returns>Rendered Solidity code.</returns>
        private string RenderWithTemplate(string templateName, FunctionRenderingModel model)
        {
            try
            {
                // find the appropriate template by name
                var template = GetTemplateByName(templateName);
                if (template == null)
                {
                    _debugInfo.AppendLine($"// WARNING: Template '{templateName}' not found, using default");
                    return Render((object)model);
                }
                return template.Render(model, member => member.Name);
            }
            catch (Exception ex)
            {
                _debugInfo.AppendLine($"// ERROR in RenderWithTemplate({templateName}): {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Gets a Scriban template by name.
        /// </summary>
        /// <param name="templateName">Template name.</param>
        /// <returns>The template found or null if it doesn't exist.</returns>
        private Scriban.Template? GetTemplateByName(string templateName)
        {
            try
            {
                var assembly = typeof(FunctionProcessor).Assembly;
                var fileName = $"{templateName}.solidity.scriban";
                // i dont know if this is the right way to get the path
                var path = $"Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Templates.{fileName}";
                
                using var stream = assembly.GetManifestResourceStream(path);
                if (stream == null)
                {
                    _debugInfo.AppendLine($"// ERROR: Template file not found at '{path}'");
                    return null;
                }
                
                using var reader = new System.IO.StreamReader(stream);
                var templateText = reader.ReadToEnd();
                var template = Scriban.Template.Parse(templateText, fileName);
                
                if (template.HasErrors)
                {
                    _debugInfo.AppendLine($"// ERROR: Template parsing errors: {template.Messages}");
                    return null;
                }
                
                return template;
            }
            catch (Exception ex)
            {
                _debugInfo.AppendLine($"// ERROR in GetTemplateByName: {ex.Message}");
                return null;
            }
        }
        
        private void ValidateModel(FunctionModel model)
        {
            _debugInfo.AppendLine($"// DEBUG: Validating model");
            
            // check for special function types that shouldn't have names
            if ((model.FunctionType == SolidityFunctionTypeEnum.Fallback || 
                 model.FunctionType == SolidityFunctionTypeEnum.Receive) && 
                !string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentException($"{model.FunctionType} function must not have a name");
            }
            
            // normal functions must have a name
            if (model.FunctionType == SolidityFunctionTypeEnum.Normal && string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentException("Normal function must have a name");
            }
            
            if (model.Parameters == null)
            {
                _debugInfo.AppendLine("// WARNING: Parameters collection is null");
                model.Parameters = new List<FunctionParameterModel>();
            }
            
            if (model.ReturnParameters == null)
            {
                _debugInfo.AppendLine("// WARNING: ReturnParameters collection is null");
                model.ReturnParameters = new List<ReturnParameterModel>();
            }
            
            if (model.Modifiers == null)
            {
                _debugInfo.AppendLine("// WARNING: Modifiers collection is null");
                model.Modifiers = new List<ModifierModel>();
            }
            
            //  duplicate parameter names
            var parameterNames = model.Parameters.Select(p => p.Name).ToList();
            var duplicateNames = parameterNames
                .GroupBy(name => name)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();
                
            if (duplicateNames.Any())
            {
                throw new ArgumentException($"Duplicate parameter names: {string.Join(", ", duplicateNames)}");
            }
            
            // check interface-specific rules
            if (model.IsInterfaceDeclaration)
            {
                ValidateInterfaceFunction(model);
            }
            
            // validate function-type specific rules
            ValidateFunctionSpecifics(model);
            
            // validate memory locations for external functions
            if (model.Visibility == SolidityVisibilityEnum.External)
            {
                ValidateExternalFunctionMemoryLocations(model);
            }
            
            _debugInfo.AppendLine("// DEBUG: Model validated");
        }
        
        private void ValidateInterfaceFunction(FunctionModel model)
        {
            // interface functions must be external
            if (model.Visibility != SolidityVisibilityEnum.External)
            {
                throw new ArgumentException("Interface functions must be external");
            }
            
            // interface functions must not have a body
            if (model.Body?.Statements != null && model.Body.Statements.Count > 0)
            {
                throw new ArgumentException("Interface functions must not have a body");
            }
            
            // interface functions cannot be virtual (they are implicitly virtual)
            if (model.IsVirtual)
            {
                throw new ArgumentException("Interface functions cannot be explicitly marked as virtual");
            }
            
            // interface functions cannot be override
            if (model.IsOverride)
            {
                throw new ArgumentException("Interface functions cannot be marked as override");
            }
        }
        private void ValidateExternalFunctionMemoryLocations(FunctionModel model)
        {
            // for external functions, reference type parameters cannot use storage location
            foreach (var parameter in model.Parameters)
            {
                if (parameter.Location == SolidityMemoryLocation.Storage)
                {
                    throw new ArgumentException($"External function parameter '{parameter.Name}' cannot use 'storage' location");
                }
            }
            
            // same rule applies to return values
            foreach (var returnParam in model.ReturnParameters)
            {
                if (returnParam.Location == SolidityMemoryLocation.Storage)
                {
                    var paramName = string.IsNullOrEmpty(returnParam.Name) ? "unnamed" : returnParam.Name;
                    throw new ArgumentException($"External function return value '{paramName}' cannot use 'storage' location");
                }
            }
        }
        
        private void ValidateFunctionSpecifics(FunctionModel model)
        {
            switch (model.FunctionType)
            {
                case SolidityFunctionTypeEnum.Fallback:
                    ValidateFallbackFunction(model);
                    break;
                    
                case SolidityFunctionTypeEnum.Receive:
                    ValidateReceiveFunction(model);
                    break;
                    
                case SolidityFunctionTypeEnum.Constructor:
                    ValidateConstructorFunction(model);
                    break;
            }
        }

        private void ValidateFallbackFunction(FunctionModel model)
        {
            // fallback must be external
            if (model.Visibility != SolidityVisibilityEnum.External)
            {
                throw new ArgumentException("Fallback function must be external");
            }
            
            // fallback cannot be view or pure
            if (model.Mutability == SolidityFunctionMutabilityEnum.View || 
                model.Mutability == SolidityFunctionMutabilityEnum.Pure)
            {
                throw new ArgumentException("Fallback function cannot be view or pure");
            }
            
            // validate fallback parameters and return values
            if (model.Parameters.Count > 1)
            {
                throw new ArgumentException("Fallback function can have at most one parameter");
            }
            
            // if fallback has no parameters, it cannot have return values
            if (model.Parameters.Count == 0 && model.ReturnParameters.Count > 0)
            {
                throw new ArgumentException("Fallback function without parameters cannot have return values");
            }
            
            // if fallback has one parameter, it must be 'bytes calldata'
            if (model.Parameters.Count == 1)
            {
                var param = model.Parameters[0];
                var dataType = SolidityReferenceTypeSyntaxHelper.RenderTypeReference(param.Type);
                var location = param.Location.HasValue ? param.Location.Value : SolidityMemoryLocation.None;
                
                if (dataType != "bytes" || location != SolidityMemoryLocation.Calldata)
                {
                    throw new ArgumentException("Fallback function parameter must be 'bytes calldata'");
                }
                
                // if fallback has a parameter, it must return 'bytes memory'
                if (model.ReturnParameters.Count != 1)
                {
                    throw new ArgumentException("Fallback function with parameter must return exactly one value");
                }
                
                var returnParam = model.ReturnParameters[0];
                var returnType = SolidityReferenceTypeSyntaxHelper.RenderTypeReference(returnParam.Type);
                var returnLocation = returnParam.Location.HasValue ? returnParam.Location.Value : SolidityMemoryLocation.None;
                
                if (returnType != "bytes" || returnLocation != SolidityMemoryLocation.Memory)
                {
                    throw new ArgumentException("Fallback function with parameter must return 'bytes memory'");
                }
            }
        }

        private void ValidateReceiveFunction(FunctionModel model)
        {
            // receive must be external
            if (model.Visibility != SolidityVisibilityEnum.External)
            {
                throw new ArgumentException("Receive function must be external");
            }
            
            // must be payable
            if (model.Mutability != SolidityFunctionMutabilityEnum.Payable)
            {
                throw new ArgumentException("Receive function must be payable");
            }
            
            // must not have parameters
            if (model.Parameters.Count > 0)
            {
                throw new ArgumentException("Receive function must not have parameters");
            }
            
            // and must not have return values
            if (model.ReturnParameters.Count > 0)
            {
                throw new ArgumentException("Receive function must not have return values");
            }
        }


        private void ValidateConstructorFunction(FunctionModel model)
        {
            // constructor cannot be virtual
            if (model.IsVirtual)
            {
                throw new ArgumentException("Constructor cannot be virtual");
            }
            
            // constructor cannot be override
            if (model.IsOverride)
            {
                throw new ArgumentException("Constructor cannot be override");
            }
            
            // should be internal (default) - no explicit visibility
            if (model.Visibility != SolidityVisibilityEnum.Internal)
            {
                throw new ArgumentException("Constructor cannot have explicit visibility specifier (must be internal)");
            }
            
            // cannot be view, pure, or payable
            if (model.Mutability != SolidityFunctionMutabilityEnum.None)
            {
                throw new ArgumentException("Constructor cannot be view, pure, or payable");
            }
        }
        
        private FunctionRenderingModel PrepareRenderModel(FunctionModel model)
        {
            _debugInfo.AppendLine("// DEBUG: Preparing rendering model");
            
            // format:
            
            // parameters with detailed debugging
            var parameters = FormatParameters(model.Parameters);
            foreach (var param in parameters)
            {
                _debugInfo.AppendLine($"// DEBUG: Formatted parameter: {param}");
            }
            
            // return parameters
            var returnValues = FormatReturnParameters(model.ReturnParameters);
            foreach (var retVal in returnValues)
            {
                _debugInfo.AppendLine($"// DEBUG: Formatted return value: {retVal}");
            }
            
            // modifiers
            var modifiers = FormatModifiers(model.Modifiers);
            foreach (var mod in modifiers)
            {
                _debugInfo.AppendLine($"// DEBUG: Formatted modifier: {mod}");
            }
            
            _debugInfo.AppendLine($"// DEBUG: Statements count: {model.Body?.Statements.Count ?? 0}");
            
            var renderModel = new FunctionRenderingModel
            {
                Name = model.Name ?? string.Empty,
                Parameters = parameters,
                ReturnValues = returnValues,
                Modifiers = modifiers,
                Statements = model.Body?.Statements ?? new List<StatementInfo>(),
                Visibility = FormatVisibility(model.Visibility),
                Mutability = FormatMutability(model.Mutability),
                IsVirtual = model.IsVirtual,
                IsOverride = model.IsOverride,
                IsInterface = model.IsInterfaceDeclaration,
                CustomError = model.CustomError,
                IsPayable = model.Mutability == SolidityFunctionMutabilityEnum.Payable,
                IsFallback = model.FunctionType == SolidityFunctionTypeEnum.Fallback,
                IsReceive = model.FunctionType == SolidityFunctionTypeEnum.Receive,
                OverrideSpecifier = FormatOverrideSpecifier(model.OverrideSpecifiers)
            };
            
            _debugInfo.AppendLine("// DEBUG: Rendering model prepared");
            
            return renderModel;
        }
        

        private void DebugRenderingModel(FunctionRenderingModel model)
        {
            _debugInfo.AppendLine("// DEBUG: Rendering model properties:");
            _debugInfo.AppendLine($"//   Name: '{model.Name}'");
            _debugInfo.AppendLine($"//   Visibility: '{model.Visibility}'");
            _debugInfo.AppendLine($"//   Mutability: '{model.Mutability}'");
            _debugInfo.AppendLine($"//   Parameters count: {model.Parameters.Length}");
            _debugInfo.AppendLine($"//   ReturnValues count: {model.ReturnValues.Length}");
            _debugInfo.AppendLine($"//   Modifiers count: {model.Modifiers.Length}");
            _debugInfo.AppendLine($"//   Statements count: {model.Statements.Count}");
            _debugInfo.AppendLine($"//   IsVirtual: {model.IsVirtual}");
            _debugInfo.AppendLine($"//   IsOverride: {model.IsOverride}");
            _debugInfo.AppendLine($"//   OverrideSpecifier: '{model.OverrideSpecifier}'");
            _debugInfo.AppendLine($"//   IsInterface: {model.IsInterface}");
            _debugInfo.AppendLine($"//   HasCustomError: {model.HasCustomError}");
            _debugInfo.AppendLine($"//   CustomError: '{model.CustomError}'");
            _debugInfo.AppendLine($"//   IsFallback: {model.IsFallback}");
            _debugInfo.AppendLine($"//   IsReceive: {model.IsReceive}");
            _debugInfo.AppendLine($"//   IsPayable: {model.IsPayable}");
        }
        
        private string[] FormatParameters(List<FunctionParameterModel> parameters)
        {
            if (parameters == null || parameters.Count == 0)
            {
                _debugInfo.AppendLine("// DEBUG: No parameters to format");
                return Array.Empty<string>();
            }
                
            _debugInfo.AppendLine($"// DEBUG: Formatting {parameters.Count} parameters");
            
            try 
            {
                return parameters.OrderBy(x => x.Index)
                                .Select(p => FormatParameter(p))
                                .ToArray();
            }
            catch (Exception ex)
            {
                _debugInfo.AppendLine($"// ERROR in FormatParameters: {ex.Message}");
                return Array.Empty<string>();
            }
        }
        
        private string FormatParameter(FunctionParameterModel parameter)
        {
            try
            {
                _debugInfo.AppendLine($"// DEBUG: Formatting parameter '{parameter.Name}'");
                
                var dataType = SolidityReferenceTypeSyntaxHelper.RenderTypeReference(parameter.Type);
                _debugInfo.AppendLine($"//   Type rendered as: '{dataType}'");
                
                // add memory location if necessary
                var memoryLocation = string.Empty;
                if (parameter.Location.HasValue && parameter.Location.Value != SolidityMemoryLocation.None)
                {
                    memoryLocation = $" {parameter.Location.Value.ToString().ToLowerInvariant()}";
                    _debugInfo.AppendLine($"//   Memory location: '{memoryLocation}'");
                }
                
                var result = $"{dataType}{memoryLocation} {parameter.Name}";
                _debugInfo.AppendLine($"//   Final parameter: '{result}'");
                
                return result;
            }
            catch (Exception ex)
            {
                _debugInfo.AppendLine($"// ERROR formatting parameter '{parameter.Name}': {ex.Message}");
                throw new InvalidOperationException($"Error formatting parameter '{parameter.Name}': {ex.Message}", ex);
            }
        }
        
        private string[] FormatReturnParameters(List<ReturnParameterModel> parameters)
        {
            if (parameters == null || parameters.Count == 0)
            {
                _debugInfo.AppendLine("// DEBUG: No return parameters to format");
                return Array.Empty<string>();
            }
                
            _debugInfo.AppendLine($"// DEBUG: Formatting {parameters.Count} return parameters");
            
            try 
            {
                return parameters.OrderBy(x => x.Index)
                                .Select(p => FormatReturnParameter(p))
                                .ToArray();
            }
            catch (Exception ex)
            {
                _debugInfo.AppendLine($"// ERROR in FormatReturnParameters: {ex.Message}");
                return Array.Empty<string>();
            }
        }

        private string FormatReturnParameter(ReturnParameterModel parameter)
        {
            try
            {
                _debugInfo.AppendLine($"// DEBUG: Formatting return parameter '{parameter.Name}'");
                
                var dataType = SolidityReferenceTypeSyntaxHelper.RenderTypeReference(parameter.Type);
                _debugInfo.AppendLine($"//   Type rendered as: '{dataType}'");
                
                // memory location if necessary
                var memoryLocation = string.Empty;
                if (parameter.Location.HasValue && parameter.Location.Value != SolidityMemoryLocation.None)
                {
                    memoryLocation = $" {parameter.Location.Value.ToString().ToLowerInvariant()}";
                    _debugInfo.AppendLine($"//   Memory location: '{memoryLocation}'");
                }
                
                // return parameters can omit names
                string result = string.IsNullOrEmpty(parameter.Name) 
                    ? $"{dataType}{memoryLocation}" 
                    : $"{dataType}{memoryLocation} {parameter.Name}";
                
                _debugInfo.AppendLine($"//   Final return parameter: '{result}'");
                
                return result;
            }
            catch (Exception ex)
            {
                var paramName = string.IsNullOrEmpty(parameter.Name) ? "unnamed" : parameter.Name;
                _debugInfo.AppendLine($"// ERROR formatting return parameter '{paramName}': {ex.Message}");
                throw new InvalidOperationException($"Error formatting return parameter '{paramName}': {ex.Message}", ex);
            }
        }

        private string[] FormatModifiers(List<ModifierModel> modifiers)
        {
            if (modifiers == null || modifiers.Count == 0)
            {
                _debugInfo.AppendLine("// DEBUG: No modifiers to format");
                return Array.Empty<string>();
            }
            
            _debugInfo.AppendLine($"// DEBUG: Formatting {modifiers.Count} modifiers");
            
            try 
            {
                return modifiers.Select(m => FormatModifier(m)).ToArray();
            }
            catch (Exception ex)
            {
                _debugInfo.AppendLine($"// ERROR in FormatModifiers: {ex.Message}");
                return Array.Empty<string>();
            }
        }

        private string FormatModifier(ModifierModel modifier)
        {
            if (modifier == null)
            {
                _debugInfo.AppendLine("// WARNING: Null modifier");
                return string.Empty;
            }
            
            try
            {
                _debugInfo.AppendLine($"// DEBUG: Formatting modifier '{modifier.Name}'");
                
                //return the name if no arguments
                if (modifier.Arguments == null || modifier.Arguments.Count == 0)
                {
                    _debugInfo.AppendLine($"//   No arguments, returning name: '{modifier.Name}'");
                    return modifier.Name;
                }
                
                // format arguments
                var args = string.Join(", ", modifier.Arguments);
                var result = $"{modifier.Name}({args})";
                _debugInfo.AppendLine($"//   Final modifier with args: '{result}'");
                
                return result;
            }
            catch (Exception ex)
            {
                _debugInfo.AppendLine($"// ERROR formatting modifier '{modifier.Name}': {ex.Message}");
                throw new InvalidOperationException($"Error formatting modifier '{modifier.Name}': {ex.Message}", ex);
            }
        }
        
        private string FormatOverrideSpecifier(List<string>? overrideSpecifiers)
        {
            if (overrideSpecifiers == null || overrideSpecifiers.Count == 0)
            {
                return string.Empty;
            }
            
            return $"({string.Join(", ", overrideSpecifiers)})";
        }

        private string FormatVisibility(SolidityVisibilityEnum visibility)
        {
            var result = visibility.ToString().ToLowerInvariant();
            _debugInfo.AppendLine($"// DEBUG: Visibility formatted as: '{result}'");
            return result;
        }


        private string FormatMutability(SolidityFunctionMutabilityEnum mutability)
        {
            string result = mutability switch
            {
                SolidityFunctionMutabilityEnum.View => "view",
                SolidityFunctionMutabilityEnum.Pure => "pure",
                SolidityFunctionMutabilityEnum.Payable => "payable",
                _ => string.Empty // No mutability (non-payable)
            };
            
            _debugInfo.AppendLine($"// DEBUG: Mutability formatted as: '{result}'");
            return result;
        }
        
        public string GetDebugInfo()
        {
            return _debugInfo.ToString();
        }
    }
}