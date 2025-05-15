using System.Collections.Generic;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.ScribanRenderingModels
{
    public class FunctionRenderingModel
    {
        public required string Name { get; set; }
        public required string Visibility { get; set; }
        public required string Mutability { get; set; }
        public bool IsVirtual { get; set; }
        public bool IsOverride { get; set; }
        public required string OverrideSpecifier { get; set; } = string.Empty;
        public bool IsInterface { get; set; }
        public required string CustomError { get; set; } = string.Empty;
        public bool HasCustomError { get; set; }
        public bool IsReceive { get; set; }
        public bool IsFallback { get; set; }
        public bool IsPayable { get; set; }
        
        public bool HasParameters { get; set; }
        public string[] Parameters { get; set; } = new string[0];
        
        public bool HasReturnValues { get; set; }
        public string[] ReturnValues { get; set; } = new string[0];
        
        public bool HasModifiers { get; set; }
        public string[] Modifiers { get; set; } = new string[0];
        
        public bool HasStatements { get; set; }
        public List<string> Statements { get; set; } = new List<string>();
    }
}