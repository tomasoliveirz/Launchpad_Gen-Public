using System;
using System.Collections.Generic;
using System.Linq;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Base;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Functions.Body;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Functions
{
    /// <summary>
    /// Represents the body of a Solidity function as a collection of statements.
    /// </summary>
    public class FunctionBodyModel : SolidityModel
    {

        public List<StatementInfo> Statements { get; } = new();

        public void AddRequireStatement(string condition, string? message = null)
        {
            Statements.Add(new StatementInfo
            {
                Type = StatementType.Require,
                Condition = condition,
                Message = message
            });
        }


        public void AddRevertStatement(string errorName, params string[] args)
        {
            Statements.Add(new StatementInfo
            {
                Type = StatementType.Revert,
                Name = errorName,
                Arguments = args.ToList()
            });
        }

        public IfStatementInfo AddIfStatement(string condition)
        {
            var ifStatement = new IfStatementInfo
            {
                Condition = condition
            };
            
            Statements.Add(ifStatement);
            return ifStatement;
        }
        
        public void AddMethodCall(string methodName, params string[] args)
        {
            Statements.Add(new StatementInfo
            {
                Type = StatementType.MethodCall,
                Name = methodName,
                Arguments = args.ToList()
            });
        }

        public void AddAssignment(string target, string value, string? operatorSymbol = "=")
        {
            Statements.Add(new StatementInfo
            {
                Type = StatementType.Assignment,
                Target = target,
                Value = value,
                Operator = operatorSymbol ?? "="
            });
        }
        
        public void AddEmitStatement(string eventName, params string[] args)
        {
            Statements.Add(new StatementInfo
            {
                Type = StatementType.Emit,
                Name = eventName,
                Arguments = args.ToList()
            });
        }

 
        public void AddReturnStatement(params string[] values)
        {
            Statements.Add(new StatementInfo
            {
                Type = StatementType.Return,
                Arguments = values.ToList()
            });
        }


        public void AddComment(string text)
        {
            Statements.Add(new StatementInfo
            {
                Type = StatementType.Comment,
                Text = text
            });
        }
        
        public void AddRawStatement(string code)
        {
            if (code == null)
                throw new ArgumentNullException(nameof(code));
                
            Statements.Add(new StatementInfo
            {
                Type = StatementType.Raw,
                Text = code
            });
        }

        /// <summary>
        /// Factory method to create a FunctionBodyModel with predefined instructions.
        /// </summary>
        /// <param name="configure">Action to configure the function body.</param>
        /// <returns>Configured instance of FunctionBodyModel.</returns>
        public static FunctionBodyModel Create(Action<FunctionBodyModel> configure)
        {
            var body = new FunctionBodyModel();
            configure(body);
            return body;
        }
        
        /// <summary>
        /// Factory method to create a FunctionBodyModel from raw text.
        /// </summary>
        /// <param name="code">Raw code.</param>
        /// <returns>Instance of FunctionBodyModel with the specified code.</returns>
        public static FunctionBodyModel FromRawText(string code)
        {
            var body = new FunctionBodyModel();
            body.AddRawStatement(code);
            return body;
        }
    }
}