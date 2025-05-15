using System;
using System.Collections.Generic;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Helpers;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.TypeReferences;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Statements
{
    public class NewObjectAssignmentStatement : AssignmentStatementBase
    {
        public TypeReference DataType { get; set; }
        public SolidityMemoryLocation Location { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        
        protected override string TemplateBaseName => "NewObjectStatement";
        
        public NewObjectAssignmentStatement(string name, TypeReference dataType, string value, SolidityMemoryLocation location = SolidityMemoryLocation.None)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            DataType = dataType ?? throw new ArgumentNullException(nameof(dataType));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Location = location;
            Target = name;
        }
        
        public override void ProcessProperties(Dictionary<string, object> properties)
        {
            base.ProcessProperties(properties);
            
            // process data type
            if (properties.ContainsKey("DataType") && properties["DataType"] is TypeReference dataType)
            {
                properties["DataType"] = SolidityReferenceTypeSyntaxHelper.RenderTypeReference(dataType);
            }
            
            // process memory location
            if (properties.ContainsKey("Location") && properties["Location"] is SolidityMemoryLocation location && 
                location != SolidityMemoryLocation.None)
            {
                properties["Location"] = location.ToString().ToLowerInvariant();
            }
            else
            {
                properties["Location"] = string.Empty;
            }
        }
    }
}