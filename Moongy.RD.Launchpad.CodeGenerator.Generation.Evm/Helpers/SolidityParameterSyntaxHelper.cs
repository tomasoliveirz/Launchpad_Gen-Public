using System;
using System.Collections.Generic;
using System.Linq;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Parameters;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Helpers
{
    public class SolidityParameterSyntaxHelper : BaseSoliditySyntaxHelper<ParameterModel>
    {
        public override string Render(ParameterModel parameter)
        {
            return parameter switch
            {
                EventParameterModel @event => RenderEventParameter(@event),
                ConstructorParameterModel constructor => RenderConstructorParameter(constructor),
                FunctionParameterModel function => RenderFunctionParameter(function),
                ReturnParameterModel @return => RenderReturnParameter(@return),
                ErrorParameterModel error => RenderErrorParameter(error),
                ModifierParameterModel modifier => RenderModifierParameter(modifier),
                _ => throw new NotSupportedException($"Unsupported parameter type: {parameter.GetType().Name}")
            };
        }

        private string RenderErrorParameter(ErrorParameterModel error)
        {
            var typeString = SolidityReferenceTypeSyntaxHelper.RenderTypeReference(error.Type);
            return $"{typeString} {error.Name}";
        }

        private string RenderReturnParameter(ReturnParameterModel @return)
        {
            var typeString = SolidityReferenceTypeSyntaxHelper.RenderTypeReference(@return.Type);
            
            var memoryLocation = string.Empty;
            if (@return.Location.HasValue && @return.Location.Value != SolidityMemoryLocation.None)
            {
                memoryLocation = $" {@return.Location.Value.ToString().ToLowerInvariant()}";
            }
            
            if (string.IsNullOrEmpty(@return.Name))
                return $"{typeString}{memoryLocation}";
            
            return $"{typeString}{memoryLocation} {@return.Name}";
        }

        private string RenderFunctionParameter(FunctionParameterModel function)
        {
            var typeString = SolidityReferenceTypeSyntaxHelper.RenderTypeReference(function.Type);
        
            var memoryLocation = string.Empty;
            if (function.Location.HasValue && function.Location.Value != SolidityMemoryLocation.None)
            {
                memoryLocation = $" {function.Location.Value.ToString().ToLowerInvariant()}";
            }
        
            return $"{typeString}{memoryLocation} {function.Name}";
        }

        private string RenderEventParameter(EventParameterModel @event)
        {
            var typeString = SolidityReferenceTypeSyntaxHelper.RenderTypeReference(@event.Type);
            var indexed = @event.IsIndexed ? "indexed " : "";
            return $"{typeString} {indexed}{@event.Name}";
        }

        private string RenderConstructorParameter(ConstructorParameterModel constructor)
        {
            var typeString = SolidityReferenceTypeSyntaxHelper.RenderTypeReference(constructor.Type);
            
            var memoryLocation = string.Empty;
            if (constructor.Location.HasValue && constructor.Location.Value != SolidityMemoryLocation.None)
            {
                memoryLocation = $" {constructor.Location.Value.ToString().ToLowerInvariant()}";
            }
            
            return $"{typeString}{memoryLocation} {constructor.Name}";
        }
        
        private string RenderModifierParameter(ModifierParameterModel modifier)
        {
            var typeString = SolidityReferenceTypeSyntaxHelper.RenderTypeReference(modifier.Type);
            
            var memoryLocation = string.Empty;
            if (modifier.Location.HasValue && modifier.Location.Value != SolidityMemoryLocation.None)
            {
                memoryLocation = $" {modifier.Location.Value.ToString().ToLowerInvariant()}";
            }
            
            return $"{typeString}{memoryLocation} {modifier.Name}";
        }

        public string[] Render(ParameterModel[] parameters)
        {
            return parameters.Select(Render).ToArray();
        }
    }
}