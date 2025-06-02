using Moongy.RD.Launchpad.CodeGenerator.Core.Interfaces;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Models.BuyBack;

namespace Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Augmenters
{
    public class BuyBackAugmenter : IAugmenter<BuyBackTokenomicModel>
    {

        private FieldDefinition _feeStateVariable = new() { Name = "_buybackFeePercent", Type = new TypeReference() { Primitive = PrimitiveType.Uint256 }, Visibility = Visibility.Private };
        private FieldDefinition _thresholdStateVariable = new() { Name = "_buybackThreshold", Type = new TypeReference() { Primitive = PrimitiveType.Uint256 }, Visibility = Visibility.Private };
        private FieldDefinition _accumulatedStateVariable = new() { Name = "_buybackAccumulated", Type = new TypeReference() { Primitive = PrimitiveType.Uint256 }, Visibility = Visibility.Private };
        private FieldDefinition _uniswapV2Router = new() { Name = "uniswapV2Router", Type = new TypeReference() { Primitive = PrimitiveType.Address }, Visibility = Visibility.Private };
        private ExpressionDefinition _require = new ExpressionDefinition() { MemberName = "require" };
        private TypeReference uint256 = new TypeReference() { Primitive = PrimitiveType.Uint256 };
        private const byte MAX_FEE = 35;

        public void Augment(ContextMetamodel context, BuyBackTokenomicModel model)
        {
            AddStateVariables(context);
            InitializeVariables(context, model);
            AddAccessFunctions(context);
            // add functions
            // change transfer function
        }


        private void AddBuybackFunction(ContextMetamodel context)
        {
            var contract = context.Modules.First();
            var tokenAmount = new ParameterDefinition() { Name = "tokenAmount", Type = uint256 };

            var body = new List<FunctionStatementDefinition>();

            var approveCall = new FunctionStatementDefinition
            {
                Kind = FunctionStatementKind.Expression,
                Expression = new ExpressionDefinition
                {
                    Kind = ExpressionKind.FunctionCall,
                    Callee = new ExpressionDefinition
                    {
                        Kind = ExpressionKind.Identifier,
                        Identifier = "_approve"
                    },
                    Arguments = [
                        new() {Kind = ExpressionKind.Literal, LiteralValue= "address(this)"},
                        new() {Kind = ExpressionKind.Identifier, Identifier = _uniswapV2Router.Name},
                        new() {Kind = ExpressionKind.Identifier, Identifier = tokenAmount.Name}]
                }
            };
            body.Add(approveCall);

            var arrayType = new TypeReference
            {
                Kind = TypeReferenceKind.Array,
                Primitive = PrimitiveType.String,
            };

           // set path values

            // execute swap

            var function = new FunctionDefinition() { Name = "_executeBuyBack", Visibility = Visibility.Internal, Parameters = [tokenAmount], Body = body };
        }

        private void AddAccessFunctions(ContextMetamodel context)
        {
            var contract = context.Modules.First();
            var modifier = contract.Modifiers.FirstOrDefault();
            if (modifier == null) throw new Exception("NO MODIFIER FOUND");
            foreach (var item in new List<FieldDefinition>() { _feeStateVariable, _thresholdStateVariable, _uniswapV2Router })
            {
                var readFunction = new FunctionDefinition() { Visibility = Visibility.Public, Name = "get_"+item.Name, ReturnParameters = [new() { Name = item.Name, Type = item.Type }], Kind = FunctionKind.Normal };
                var writeFunction = new FunctionDefinition() { Name = "set_" + item.Name, Kind = FunctionKind.Normal, Modifiers = [modifier], Parameters = [new() { Name = item.Name.Substring(1), Type = item.Type }] };
                AddFunctionBody(writeFunction, item);
                contract.Functions.Add(readFunction);
                contract.Functions.Add(writeFunction);
            }
            var accumulatedReadFunction = new FunctionDefinition() { Visibility = Visibility.Public, Name = "get_" + _accumulatedStateVariable.Name, ReturnParameters = [new() { Name = _accumulatedStateVariable.Name, Type = _accumulatedStateVariable.Type }], Kind = FunctionKind.Normal };
            contract.Functions.Add(accumulatedReadFunction);
        }

        private void AddFunctionBody(FunctionDefinition def, FieldDefinition field)
        {
            if (field == _feeStateVariable) AddFeeBody(def, field);
            else if (field == _thresholdStateVariable) AddThresholdBody(def, field);
            else if (field == _uniswapV2Router) AddRouterBody(def, field);
        }

        private void AddFieldAssignment(FunctionDefinition def, FieldDefinition field)
        {
            var assignment = new AssignmentDefinition()
            {
                Left = new ExpressionDefinition() { Identifier = field.Name },
                Right = new ExpressionDefinition() { Identifier = def.Parameters.FirstOrDefault()!.Name! }
            };

            var assign = new FunctionStatementDefinition()
            {
                Kind = FunctionStatementKind.Assignment,
                ParameterAssignment = assignment
            };
            def.Body.Add(assign);

        }

        private void AddRouterBody(FunctionDefinition def, FieldDefinition field)
        {
            var require = new FunctionStatementDefinition()
            {
                Expression = new()
                {
                    Callee = _require,
                    Kind = ExpressionKind.FunctionCall,
                    Arguments = [ new() {
                        Kind = ExpressionKind.Binary,
                        Operator = BinaryOperator.NotEqual,
                        Left = new(){Kind = ExpressionKind.MemberAccess, MemberName= def.Parameters.FirstOrDefault()!.Name },
                        Right = new(){Kind = ExpressionKind.FunctionCall, MemberName= "address", Arguments=[new(){Kind= ExpressionKind.Literal, LiteralValue="0" }]}
                    },
                    new() { Kind= ExpressionKind.Literal, LiteralValue = "Router cannot be zero"}
                    ]
                }
            };
                    
            def.Body.Add(require);
            AddFieldAssignment(def, field);
        }

        private void AddThresholdBody(FunctionDefinition def, FieldDefinition field)
        {
            AddFieldAssignment(def, field);
        }

        private void AddFeeBody(FunctionDefinition def, FieldDefinition field)
        {
            var require = new FunctionStatementDefinition()
            {
                Expression = new()
                {
                    Callee = _require,
                    Kind = ExpressionKind.FunctionCall,
                    Arguments = [ new() {
                        Kind = ExpressionKind.Binary,
                        Operator = BinaryOperator.NotEqual,
                        Left = new(){Kind = ExpressionKind.MemberAccess, MemberName= def.Parameters.FirstOrDefault()!.Name },
                        Right = new(){Kind = ExpressionKind.Literal, LiteralValue=MAX_FEE.ToString()}
                    },
                    new() { Kind= ExpressionKind.Literal, LiteralValue = "Router cannot be zero"}
                    ]
                }
            }
            def.Body.Add(require);

            AddFieldAssignment(def, field);
        }

        private void AddStateVariables(ContextMetamodel context)
        {
           var contract = context.Modules.First();
            contract.Fields.AddRange([_feeStateVariable, _thresholdStateVariable, _accumulatedStateVariable, _uniswapV2Router]);
        }

        private void InitializeVariables(ContextMetamodel context, BuyBackTokenomicModel model)
        {
            var contract = context.Modules.First();
            var constructor = contract.Functions.FirstOrDefault(x => x.Kind == FunctionKind.Constructor);
            if(constructor == null)
            {
                constructor = new FunctionDefinition() { Kind = FunctionKind.Constructor, Name="ctor" };
                contract.Functions.Add(constructor);
            }
            var feeAssign = new AssignmentDefinition()
            {
                Left = new ExpressionDefinition() { Identifier = _feeStateVariable.Name },
                Right = new ExpressionDefinition() { LiteralValue = model.BuyBackFeePercent.ToString() }
            };
            var feeAssignStatement = new FunctionStatementDefinition() { Kind = FunctionStatementKind.Assignment, ParameterAssignment = feeAssign };


            var thresholdAssign = new AssignmentDefinition()
            {
                Left = new ExpressionDefinition() { Identifier = _thresholdStateVariable.Name },
                Right = new ExpressionDefinition() { LiteralValue = $"{model.BuyBackThreshold.ToString()} * 10**decimals()" }
            };


            var accumulatedAssign = new AssignmentDefinition()
            {
                Left = new ExpressionDefinition() { Identifier = _accumulatedStateVariable.Name },
                Right = new ExpressionDefinition() { LiteralValue = "0" }
            };
            var uniswapRouterAssign = new AssignmentDefinition()
            {
                Left = new ExpressionDefinition() { Identifier = _uniswapV2Router.Name },
                Right = new ExpressionDefinition() { LiteralValue = "0x0" }
            };

            foreach( var item in new List<AssignmentDefinition>() { feeAssign, thresholdAssign, accumulatedAssign, uniswapRouterAssign })
            {
                var statement = new FunctionStatementDefinition()
                {
                    Kind = FunctionStatementKind.Assignment,
                    ParameterAssignment = item
                };
                constructor.Body.Add(statement);
            }

        }

    }
}
