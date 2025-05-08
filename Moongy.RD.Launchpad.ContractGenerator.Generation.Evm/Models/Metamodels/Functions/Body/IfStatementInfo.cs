namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Functions.Body
{
    /// <summary>
    /// Represents information of an if/else statement in the body of a Solidity function.
    /// </summary>
    public class IfStatementInfo : StatementInfo
    {
        public IfStatementInfo()
        {
            Type = StatementType.If;
        }
        
        public void AddThenStatement(StatementInfo statement)
        {
            ThenStatements.Add(statement);
        }
        
        public void AddThenRequireStatement(string condition, string? message = null)
        {
            ThenStatements.Add(new StatementInfo
            {
                Type = StatementType.Require,
                Condition = condition,
                Message = message
            });
        }
        
        public void AddThenRevertStatement(string errorName, params string[] args)
        {
            ThenStatements.Add(new StatementInfo
            {
                Type = StatementType.Revert,
                Name = errorName,
                Arguments = new List<string>(args)
            });
        }

        public void AddThenMethodCall(string methodName, params string[] args)
        {
            ThenStatements.Add(new StatementInfo
            {
                Type = StatementType.MethodCall,
                Name = methodName,
                Arguments = new List<string>(args)
            });
        }
        
        public void AddThenAssignment(string target, string value, string? operatorSymbol = "=")
        {
            ThenStatements.Add(new StatementInfo
            {
                Type = StatementType.Assignment,
                Target = target,
                Value = value,
                Operator = operatorSymbol ?? "="
            });
        }

        public void AddThenReturnStatement(params string[] values)
        {
            ThenStatements.Add(new StatementInfo
            {
                Type = StatementType.Return,
                Arguments = new List<string>(values)
            });
        }
        

        public void AddElseStatement(StatementInfo statement)
        {
            ElseStatements.Add(statement);
        }

        public void AddElseRequireStatement(string condition, string? message = null)
        {
            ElseStatements.Add(new StatementInfo
            {
                Type = StatementType.Require,
                Condition = condition,
                Message = message
            });
        }

        public void AddElseRevertStatement(string errorName, params string[] args)
        {
            ElseStatements.Add(new StatementInfo
            {
                Type = StatementType.Revert,
                Name = errorName,
                Arguments = new List<string>(args)
            });
        }

        public void AddElseMethodCall(string methodName, params string[] args)
        {
            ElseStatements.Add(new StatementInfo
            {
                Type = StatementType.MethodCall,
                Name = methodName,
                Arguments = new List<string>(args)
            });
        }

        public void AddElseAssignment(string target, string value, string? operatorSymbol = "=")
        {
            ElseStatements.Add(new StatementInfo
            {
                Type = StatementType.Assignment,
                Target = target,
                Value = value,
                Operator = operatorSymbol ?? "="
            });
        }

        public void AddElseReturnStatement(params string[] values)
        {
            ElseStatements.Add(new StatementInfo
            {
                Type = StatementType.Return,
                Arguments = new List<string>(values)
            });
        }
    }
}