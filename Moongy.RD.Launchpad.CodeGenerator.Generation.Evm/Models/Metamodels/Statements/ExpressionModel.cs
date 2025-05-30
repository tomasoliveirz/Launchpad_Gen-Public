using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Statements
{
    /// <summary>
    /// Base abstract class for Solidity expressions.
    /// </summary>
    public abstract class ExpressionModel : SolidityModel
    {
        public abstract override string ToString();
        
        public BinaryExpressionModel Binary(OperatorEnum op, ExpressionModel right)
        {
            return new BinaryExpressionModel(this, op, right);
        }
        
        public BinaryExpressionModel Binary(OperatorEnum op, string right)
        {
            return new BinaryExpressionModel(this, op, new LiteralExpressionModel(right));
        }
        
        public BinaryExpressionModel Equal(ExpressionModel right)
        {
            return Binary(OperatorEnum.Equal, right);
        }
        
        public BinaryExpressionModel Equal(string right)
        {
            return Binary(OperatorEnum.Equal, right);
        }
        
        public BinaryExpressionModel NotEqual(ExpressionModel right)
        {
            return Binary(OperatorEnum.Different, right);
        }
        
        public BinaryExpressionModel NotEqual(string right)
        {
            return Binary(OperatorEnum.Different, right);
        }
        
        public BinaryExpressionModel GreaterThan(ExpressionModel right)
        {
            return Binary(OperatorEnum.GreaterThan, right);
        }
        public BinaryExpressionModel GreaterThan(string right)
        {
            return Binary(OperatorEnum.GreaterThan, right);
        }
        
        public BinaryExpressionModel GreaterOrEqual(ExpressionModel right)
        {
            return Binary(OperatorEnum.GreaterOrEqualTo, right);
        }
        
        public BinaryExpressionModel GreaterOrEqual(string right)
        {
            return Binary(OperatorEnum.GreaterOrEqualTo, right);
        }

        public BinaryExpressionModel LessThan(ExpressionModel right)
        {
            return Binary(OperatorEnum.LessThan, right);
        }
        

        public BinaryExpressionModel LessThan(string right)
        {
            return Binary(OperatorEnum.LessThan, right);
        }

        public BinaryExpressionModel LessOrEqual(ExpressionModel right)
        {
            return Binary(OperatorEnum.LessThanOrEqualTo, right);
        }
        
        public BinaryExpressionModel LessOrEqual(string right)
        {
            return Binary(OperatorEnum.LessThanOrEqualTo, right);
        }

        public BinaryExpressionModel And(ExpressionModel right)
        {
            return Binary(OperatorEnum.And, right);
        }
        public BinaryExpressionModel Or(ExpressionModel right)
        {
            return Binary(OperatorEnum.Or, right);
        }

        public UnaryExpressionModel Not()
        {
            return new UnaryExpressionModel(OperatorEnum.Not, this);
        }
        
        // static factory method to create an expression from a literal string
        public static ExpressionModel FromString(string expression)
        {
            return new LiteralExpressionModel(expression);
        }
        
        //static factory method to create an equal comparison expression
        public static BinaryExpressionModel Equal(string left, string right)
        {
            return new BinaryExpressionModel(
                new LiteralExpressionModel(left),
                OperatorEnum.Equal,
                new LiteralExpressionModel(right)
            );
        }
        

        // factory method to create a not equal comparison expression
        public static BinaryExpressionModel NotEqual(string left, string right)
        {
            return new BinaryExpressionModel(
                new LiteralExpressionModel(left),
                OperatorEnum.Different,
                new LiteralExpressionModel(right)
            );
        }
        
        // create a greater than comparison expression

        public static BinaryExpressionModel GreaterThan(string left, string right)
        {
            return new BinaryExpressionModel(
                new LiteralExpressionModel(left),
                OperatorEnum.GreaterThan,
                new LiteralExpressionModel(right)
            );
        }

        // create a greater than or equal comparison expression
        public static BinaryExpressionModel GreaterOrEqual(string left, string right)
        {
            return new BinaryExpressionModel(
                new LiteralExpressionModel(left),
                OperatorEnum.GreaterOrEqualTo,
                new LiteralExpressionModel(right)
            );
        }
        

        // create a less than comparison expression
        public static BinaryExpressionModel LessThan(string left, string right)
        {
            return new BinaryExpressionModel(
                new LiteralExpressionModel(left),
                OperatorEnum.LessThan,
                new LiteralExpressionModel(right)
            );
        }
        
        // create a less than or equal comparison expression
        public static BinaryExpressionModel LessOrEqual(string left, string right)
        {
            return new BinaryExpressionModel(
                new LiteralExpressionModel(left),
                OperatorEnum.LessThanOrEqualTo,
                new LiteralExpressionModel(right)
            );
        }
        

        //create a logical AND expression
        public static BinaryExpressionModel And(ExpressionModel left, ExpressionModel right)
        {
            return new BinaryExpressionModel(
                left,
                OperatorEnum.And,
                right
            );
        }
        
        //create a logical OR expression
        public static BinaryExpressionModel Or(ExpressionModel left, ExpressionModel right)
        {
            return new BinaryExpressionModel(
                left,
                OperatorEnum.Or,
                right
            );
        }
        

        // create a logical NOT expression
        public static UnaryExpressionModel Not(ExpressionModel expression)
        {
            return new UnaryExpressionModel(OperatorEnum.Not, expression);
        }
    }
    
    /// <summary>
    /// Represents a literal expression in Solidity (a variable name or a constant value).
    /// </summary>
    public class LiteralExpressionModel : ExpressionModel
    {
        public string Value { get; set; }
        public LiteralExpressionModel(string value)
        {
            Value = value;
        }
        public override string ToString()
        {
            return Value;
        }
    }
    
    public class IdentifierExpressionModel : ExpressionModel
    {
        public string Identifier { get; set; }

        public IdentifierExpressionModel(string identifier)
        {
            Identifier = identifier;
        }

        public override string ToString()
        {
            return Identifier;
        }
    }
    /// <summary>
    /// Represents a binary expression in Solidity (e.g., left == right).
    /// </summary>
    public class BinaryExpressionModel : ExpressionModel
    {
        public ExpressionModel Left { get; set; }
        public OperatorEnum Operator { get; set; }
        public ExpressionModel Right { get; set; }
        
        // creates a new binary expression.
        public BinaryExpressionModel(ExpressionModel left, OperatorEnum op, ExpressionModel right)
        {
            Left = left;
            Operator = op;
            Right = right;
        }
        public override string ToString()
        {
            var opString = Operator switch
            {
                OperatorEnum.And => "&&",
                OperatorEnum.Or => "||",
                OperatorEnum.Not => "!",
                OperatorEnum.GreaterThan => ">",
                OperatorEnum.GreaterOrEqualTo => ">=",
                OperatorEnum.LessThan => "<",
                OperatorEnum.LessThanOrEqualTo => "<=",
                OperatorEnum.Different => "!=",
                OperatorEnum.Equal => "==",
                OperatorEnum.Add => "+",
                OperatorEnum.Subtract => "-",
                OperatorEnum.Multiply => "*",
                OperatorEnum.Divide => "/",
                OperatorEnum.Modulo => "%",
                _ => "=="
            };
            
            return $"{Left} {opString} {Right}";
        }
    }
    
    /// <summary>
    /// Represents a unary expression in Solidity (e.g., !expr).
    /// </summary>
    public class UnaryExpressionModel : ExpressionModel
    {
        public OperatorEnum Operator { get; set; }
        public ExpressionModel Operand { get; set; }
        public UnaryExpressionModel(OperatorEnum op, ExpressionModel operand)
        {
            Operator = op;
            Operand = operand;
        }
        
        public override string ToString()
        {
            var opString = Operator switch
            {
                OperatorEnum.Not => "!",
                _ => "!"
            };
            
            return $"{opString}{Operand}";
        }
    }
    
    public class MemberAccessExpressionModel : ExpressionModel
    {
        public ExpressionModel Target { get; set; }
        public string MemberName { get; set; }
        public MemberAccessExpressionModel(ExpressionModel target, string memberName)
        {
            Target = target;
            MemberName = memberName;
        }
        public override string ToString()
        {
            return $"{Target}.{MemberName}";
        }
    }

    public class IndexAccessExpressionModel : ExpressionModel
    {
        public ExpressionModel Target { get; set; }
        public ExpressionModel Index { get; set; }

        public IndexAccessExpressionModel(ExpressionModel target, ExpressionModel index)
        {
            Target = target;
            Index = index;
        }

        public override string ToString()
        {
            return $"{Target}[{Index}]";
        }
    }
    /// <summary>
    /// Represents a method call expression in Solidity
    /// </summary>
    public class MethodCallExpressionModel : ExpressionModel
    {
        public string MethodName { get; set; }
        public List<ExpressionModel> Arguments { get; set; } = new();
        
        public MethodCallExpressionModel(string methodName, params ExpressionModel[] args)
        {
            MethodName = methodName;
            if (args != null)
            {
                Arguments.AddRange(args);
            }
        } 
        
        public MethodCallExpressionModel(string methodName, params string[] args)
        {
            MethodName = methodName;
            if (args != null)
            {
                foreach (var arg in args)
                {
                    Arguments.Add(new LiteralExpressionModel(arg));
                }
            }
        }
        public override string ToString()
        {
            var args = string.Join(", ", Arguments);
            return $"{MethodName}({args})";
        }
    }
}