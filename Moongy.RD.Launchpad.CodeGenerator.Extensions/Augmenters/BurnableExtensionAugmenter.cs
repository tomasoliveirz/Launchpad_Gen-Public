using Moongy.RD.Launchpad.CodeGenerator.Core.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Modules;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Models;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Augmenters
{
    public class BurnableExtensionAugmenter : BaseExtensionAugmenter<BurnExtensionModel>
    {

        public override void Augment(ContextMetamodel ctx, BurnExtensionModel model)
        {
            var mod = Main(ctx);

            AddPublicBurnFunctions(mod);
        }

        private void AddPublicBurnFunctions(ModuleDefinition mod)
        {
            var burnFunction = new FunctionDefinition
            {
                Name = "burn",
                Kind = FunctionKind.Normal,
                Visibility = Visibility.Public,
                Parameters = new List<ParameterDefinition>
                {
                    new ParameterDefinition { Name = "amount", Type = DataTypeReference.Uint256 }
                },
                Body = new List<FunctionStatementDefinition>
                {
                    new FunctionStatementDefinition
                    {
                        Kind = FunctionStatementKind.Expression,
                        Expression = new ExpressionDefinition
                        {
                            Kind = ExpressionKind.FunctionCall,
                            Callee = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "_burn" },
                            Arguments = new List<ExpressionDefinition>
                            {
                                new ExpressionDefinition 
                                { 
                                    Kind = ExpressionKind.MemberAccess,
                                    Target = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "msg" },
                                    MemberName = "sender"
                                },
                                new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "amount" }
                            }
                        }
                    }
                }
            };

            var burnFromFunction = new FunctionDefinition
            {
                Name = "burnFrom",
                Kind = FunctionKind.Normal,
                Visibility = Visibility.Public,
                Parameters = new List<ParameterDefinition>
                {
                    new ParameterDefinition { Name = "account", Type = DataTypeReference.Address },
                    new ParameterDefinition { Name = "amount", Type = DataTypeReference.Uint256 }
                },
                Body = new List<FunctionStatementDefinition>
                {
                    new FunctionStatementDefinition
                    {
                        Kind = FunctionStatementKind.Expression,
                        Expression = new ExpressionDefinition
                        {
                            Kind = ExpressionKind.FunctionCall,
                            Callee = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "_spendAllowance" },
                            Arguments = new List<ExpressionDefinition>
                            {
                                new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "account" },
                                new ExpressionDefinition 
                                { 
                                    Kind = ExpressionKind.MemberAccess,
                                    Target = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "msg" },
                                    MemberName = "sender"
                                },
                                new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "amount" }
                            }
                        }
                    },
                    new FunctionStatementDefinition
                    {
                        Kind = FunctionStatementKind.Expression,
                        Expression = new ExpressionDefinition
                        {
                            Kind = ExpressionKind.FunctionCall,
                            Callee = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "_burn" },
                            Arguments = new List<ExpressionDefinition>
                            {
                                new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "account" },
                                new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "amount" }
                            }
                        }
                    }
                }
            };

            AddOnce(mod.Functions, f => f.Name == "burn", () => burnFunction);
            AddOnce(mod.Functions, f => f.Name == "burnFrom", () => burnFromFunction);
        }
    }
}