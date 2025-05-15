using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Helpers;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.TypeReferences;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Statements
{
    public class VariableDeclarationStatement : StatementModel
    {
        public TypeReference Type { get; set; }
        public string Name { get; set; }
        public SolidityMemoryLocation? Location { get; set; }
        public string? InitialValue { get; set; }
        public ExpressionModel? InitialValueExpression { get; set; }
        
        protected override string TemplateBaseName => "VariableDeclarationStatement";
        
        public VariableDeclarationStatement(TypeReference type, string name, string? initialValue = null, SolidityMemoryLocation? location = null)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            InitialValue = initialValue;
            Location = location;
        }
        
        public VariableDeclarationStatement(TypeReference type, string name, ExpressionModel initialValueExpression, SolidityMemoryLocation? location = null)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            InitialValueExpression = initialValueExpression ?? throw new ArgumentNullException(nameof(initialValueExpression));
            InitialValue = initialValueExpression.ToString();
            Location = location;
        }
        
        public override void ProcessProperties(Dictionary<string, object> properties)
        {
            base.ProcessProperties(properties);
            
            // process Type property
            if (properties.ContainsKey("Type") && properties["Type"] is TypeReference typeRef)
            {
                properties["Type"] = SolidityReferenceTypeSyntaxHelper.RenderTypeReference(typeRef);
            }
            
            // process Location property
            if (properties.ContainsKey("Location"))
            {
                if (properties["Location"] is SolidityMemoryLocation location && 
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
}