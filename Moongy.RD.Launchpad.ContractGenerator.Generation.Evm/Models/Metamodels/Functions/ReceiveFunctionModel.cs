using System;
using System.Collections.Generic;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Statements.Expressions;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Functions
{
    public class ReceiveFunctionModel : BaseFunctionModel
    {
        public override string TemplateName => "Receive";
        
        public ReceiveFunctionModel()
        {
            // these must be external and payable
            Visibility = SolidityVisibilityEnum.External;
            Mutability = SolidityFunctionMutabilityEnum.Payable;
        }
        
        public static ReceiveFunctionModel CreateWithEvent(string eventName, params string[] arguments)
        {
            var model = new ReceiveFunctionModel();
            
            var emitStatement = new EmitStatement(eventName);
            foreach (var arg in arguments)
            {
                emitStatement.AddStringArgument(arg);
            }
            
            model.AddStatement(emitStatement);
            return model;
        }
        
        public static ReceiveFunctionModel CreateWithStateUpdate(string stateVariable, string value)
        {
            var model = new ReceiveFunctionModel();
            model.AddStatement(new AssignmentStatement
            {
                Target = stateVariable,
                Value = value
            });
            return model;
        }
        
        public ReceiveFunctionModel WithValueCheck(string minValue)
        {
            var valueCheckComparison = ExpressionModel.GreaterOrEqual("msg.value", minValue);
            AddStatement(new RequireStatement
            {
                Condition = valueCheckComparison,
                Message = $"Minimum value required is {minValue}"
            });
                
            return this;
        }
        
        public override void Validate()
        {
            if (Visibility != SolidityVisibilityEnum.External)
            {
                throw new ArgumentException("Receive function must be external");
            }
            
            if (Mutability != SolidityFunctionMutabilityEnum.Payable)
            {
                throw new ArgumentException("Receive function must be payable");
            }
            
            if (Parameters.Count > 0)
            {
                throw new ArgumentException("Receive function must not have parameters");
            }
            
            if (ReturnParameters.Count > 0)
            {
                throw new ArgumentException("Receive function must not have return values");
            }
        }
    }
}