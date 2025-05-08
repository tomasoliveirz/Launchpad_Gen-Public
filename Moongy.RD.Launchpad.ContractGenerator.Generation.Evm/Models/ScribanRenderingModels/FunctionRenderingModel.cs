using System;
using System.Collections.Generic;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Functions.Body;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.ScribanRenderingModels
{
    public class FunctionRenderingModel
    {
        public string Name { get; set; } = string.Empty;
        
        public string Visibility { get; set; } = string.Empty;

        public string Mutability { get; set; } = string.Empty;
        public string? CustomError { get; set; }
        public List<StatementInfo> Statements { get; set; } = new();
        public string[] Parameters { get; set; } = Array.Empty<string>();
        public string[] ReturnValues { get; set; } = Array.Empty<string>();
        public string[] Modifiers { get; set; } = Array.Empty<string>();
        public bool IsVirtual { get; set; }
        public bool IsOverride { get; set; }
        public bool IsInterface { get; set; }
        public bool IsPayable { get; set; }
        public bool IsFallback { get; set; }
        public bool IsReceive { get; set; }
        public string OverrideSpecifier { get; set; } = string.Empty;
        public bool HasParameters => Parameters != null && Parameters.Length > 0;
        public bool HasReturnValues => ReturnValues != null && ReturnValues.Length > 0;
        public bool HasModifiers => Modifiers != null && Modifiers.Length > 0;
        public bool HasStatements => Statements != null && Statements.Count > 0;
        public bool HasCustomError => !string.IsNullOrEmpty(CustomError);
    }
}