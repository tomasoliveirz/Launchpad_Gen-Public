namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Statements
{
    /// <summary>
    /// Represents a raw code statement that doesn't need further processing.
    /// </summary>
    public class RawStatementModel : StatementModel
    {

        public string Code { get; set; } = string.Empty;
        
        protected override string TemplateBaseName => "RawStatement";
        
        public override void ProcessProperties(Dictionary<string, object> properties)
        {
            base.ProcessProperties(properties);
            // no additional processing needed
        }
    }
}