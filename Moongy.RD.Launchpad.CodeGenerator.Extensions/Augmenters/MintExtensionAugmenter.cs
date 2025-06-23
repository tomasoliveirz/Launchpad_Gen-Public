using Moongy.RD.Launchpad.CodeGenerator.Core.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Functions;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Modules;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Models;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Base;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Augmenters
{
    public class MintExtensionAugmenter : BaseExtensionAugmenter<MintExtensionModel>
    {

        public override void Augment(ContextMetamodel ctx, MintExtensionModel model)
        {
            var mod = Main(ctx);

            AddPublicMintFunction(mod);
        }

        private void AddPublicMintFunction(ModuleDefinition mod)
        {
            var modifier = DetermineAccessModifier(mod);

            var publicMintFunction = new FunctionDefinition
            {
                Name = "mint",
                Kind = FunctionKind.Normal,
                Visibility = Visibility.Public,
                Modifiers = new List<ModifierDefinition> { modifier },
                Parameters = new List<ParameterDefinition>
                
                {
                    new ParameterDefinition { Name = "to", Type = DataTypeReference.Address },
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
                            Callee = new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "_mint" },
                            Arguments = new List<ExpressionDefinition>
                            {
                                new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "to" },
                                new ExpressionDefinition { Kind = ExpressionKind.Identifier, Identifier = "amount" }
                            }
                        }
                    }
                }
            };

            AddOnce(mod.Functions, f => f.Name == "mint", () => publicMintFunction);
        }

        private ModifierDefinition DetermineAccessModifier(ModuleDefinition mod)
        {
            return new ModifierDefinition { Name = "onlyOwner" };
        }
    }
}