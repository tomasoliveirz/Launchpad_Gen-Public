using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Directives;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Imports;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Augmenters.AccessControl;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Models;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Forms;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Models;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Augmenters.Tax;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Models.Tax;

namespace CoreTests
{
    [TestClass]
    public class WorkingTokenGeneratorTest
    {
        /// <summary>
        /// Test complete token generation pipeline using real Forms and Models
        /// </summary>
        [TestMethod]
        public void TestCompleteTokenGeneration()
        {
            Console.WriteLine("🚀 Testing Complete Token Generation Pipeline\n");

            // =============================================
            // STEP 1: Create Form using your real structure
            // =============================================
            Console.WriteLine("📝 STEP 1: Creating Form Data...");
            var tokenForm = new FungibleTokenForm
            {
                Name = "TestToken",
                Symbol = "TEST",
                Decimals = 18,
                Premint = 1000000, // 1M tokens
                Supply = 10000000, // 10M total
                Tax = new TaxTokenomic
                {
                    TaxFee = 2.5, // 2.5% tax
                    Recipients = new List<TaxRecipient>
                    {
                        new() { Address = "0x742d35Cc6634C0532925a3b8D4d25db9c86E2", Shares = 100 }
                    }
                }
            };

            // Verify form data
            Assert.AreEqual("TestToken", tokenForm.Name);
            Assert.AreEqual("TEST", tokenForm.Symbol);
            Assert.AreEqual(18, tokenForm.Decimals);
            Assert.AreEqual(1000000, tokenForm.Premint);
            Assert.IsNotNull(tokenForm.Tax);
            Assert.AreEqual(2.5, tokenForm.Tax.TaxFee);

            Console.WriteLine($"   ✅ Token: {tokenForm.Name} ({tokenForm.Symbol})");
            Console.WriteLine($"   ✅ Premint: {tokenForm.Premint:N0} tokens");
            Console.WriteLine($"   ✅ Tax: {tokenForm.Tax?.TaxFee}%");

            // =============================================
            // STEP 2: Convert to Standard Model
            // =============================================
            Console.WriteLine("\n🔄 STEP 2: Converting Form to Standard Model...");
            var tokenModel = new FungibleTokenModel
            {
                Name = tokenForm.Name,
                Symbol = tokenForm.Symbol,
                Decimals = tokenForm.Decimals,
                Premint = (ulong)tokenForm.Premint // Convert long to ulong safely
            };

            Assert.AreEqual(tokenForm.Name, tokenModel.Name);
            Assert.AreEqual(tokenForm.Symbol, tokenModel.Symbol);
            Assert.AreEqual((ulong)tokenForm.Premint, tokenModel.Premint);

            Console.WriteLine($"   ✅ Model created with {tokenModel.Premint:N0} premint");

            // =============================================
            // STEP 3: Compose Base Contract
            // =============================================
            Console.WriteLine("\n🏗️ STEP 3: Composing Base Contract...");
            var composer = new FungibleTokenComposer();
            var moduleFileDefinition = composer.Compose(tokenModel);

            // Convert to ContextMetamodel (fix the type mismatch)
            var contextModel = new ContextMetamodel
            {
                Modules = moduleFileDefinition.Modules,
                Imports = moduleFileDefinition.Imports ?? new List<ImportDefinition>(),
                Directives = moduleFileDefinition.Directives ?? new List<DirectiveDefinition>()
            };

            var module = contextModel.Modules.First();

            // Verify base contract structure
            Assert.IsNotNull(module);
            Assert.AreEqual(tokenModel.Name, module.Name);
            Assert.IsTrue(module.Fields.Count >= 6); // Basic ERC20 fields
            Assert.IsTrue(module.Functions.Count >= 7); // Basic ERC20 functions
            Assert.IsTrue(module.Triggers.Count >= 1); // Transfer event

            Console.WriteLine($"   ✅ Base contract '{module.Name}' created");
            Console.WriteLine($"   ✅ Fields: {module.Fields.Count}");
            Console.WriteLine($"   ✅ Functions: {module.Functions.Count}");
            Console.WriteLine($"   ✅ Events: {module.Triggers.Count(t => t.Kind == TriggerKind.Log)}");

            // =============================================
            // STEP 4: Apply Access Control
            // =============================================
            Console.WriteLine("\n🔐 STEP 4: Applying Access Control...");
            var accessControlAugmenter = new AccessControlExtensionAugmenter();
            accessControlAugmenter.Augment(contextModel, new AccessControlExtensionModel());

            // Verify access control was applied
            Assert.IsTrue(module.Modifiers.Count > 0);
            Assert.IsTrue(module.Modifiers.Any(m => m.Name == "onlyOwner"));

            Console.WriteLine($"   ✅ Access control applied");
            Console.WriteLine($"   ✅ Modifiers: {module.Modifiers.Count}");

            // =============================================
            // STEP 5: Apply Tax Tokenomics
            // =============================================
            if (tokenForm.Tax != null)
            {
                Console.WriteLine("\n💰 STEP 5: Applying Tax Tokenomics...");
                var taxModel = new TaxTokenomicModel
                {
                    TaxFee = tokenForm.Tax.TaxFee,
                    TaxRecipients = tokenForm.Tax.Recipients
                };

                var taxAugmenter = new TaxTokenomicAugmenter();
                taxAugmenter.Augment(contextModel, taxModel);

                // Verify tax system was applied
                Assert.IsTrue(module.Fields.Any(f => f.Name == "taxFee"));
                Assert.IsTrue(module.Functions.Any(f => f.Name == "getTaxFee"));
                Assert.IsTrue(module.Functions.Any(f => f.Name == "setTaxFee"));

                Console.WriteLine($"   ✅ Tax system applied with {taxModel.TaxFee}% fee");
                Console.WriteLine($"   ✅ Tax recipients: {taxModel.TaxRecipients.Count}");
                Console.WriteLine($"   ✅ Total functions: {module.Functions.Count}");
            }

            // =============================================
            // STEP 6: Verify Final Structure
            // =============================================
            Console.WriteLine("\n🔍 STEP 6: Verifying Final Contract Structure...");

            // Verify essential fields exist
            var requiredFields = new[]
                { "_name", "_symbol", "_decimals", "_totalSupply", "_balances", "owner", "taxFee" };
            foreach (var fieldName in requiredFields)
            {
                Assert.IsTrue(module.Fields.Any(f => f.Name == fieldName), $"Missing field: {fieldName}");
            }

            // Verify essential functions exist
            var requiredFunctions = new[]
            {
                "constructor", "_transfer", "transfer", "name", "symbol", "decimals", "totalSupply", "balanceOf",
                "getTaxFee", "setTaxFee"
            };
            foreach (var functionName in requiredFunctions)
            {
                Assert.IsTrue(module.Functions.Any(f => f.Name == functionName), $"Missing function: {functionName}");
            }

            // Verify events exist
            Assert.IsTrue(module.Triggers.Any(t => t.Name == "Transfer"), "Missing Transfer event");

            Console.WriteLine($"   ✅ All required fields present: {requiredFields.Length}");
            Console.WriteLine($"   ✅ All required functions present: {requiredFunctions.Length}");
            Console.WriteLine($"   ✅ Transfer event present");

            // =============================================
            // STEP 7: Display Final Result
            // =============================================
            Console.WriteLine("\n📋 STEP 7: Final Contract Summary");
            DisplayContractSummary(contextModel);

            Console.WriteLine("\n🎉 Token Generation Test Completed Successfully!");
        }

        private void DisplayContractSummary(ContextMetamodel context)
        {
            var module = context.Modules.First();

            Console.WriteLine($"\n📄 Contract: {module.Name}");
            Console.WriteLine($"   📊 Fields: {module.Fields.Count}");
            Console.WriteLine($"   🔧 Modifiers: {module.Modifiers.Count}");
            Console.WriteLine($"   ⚡ Events: {module.Triggers.Count(t => t.Kind == TriggerKind.Log)}");
            Console.WriteLine($"   🔨 Functions: {module.Functions.Count}");

            // List key functions
            var keyFunctions = module.Functions.Where(f =>
                    new[] { "constructor", "transfer", "_transfer", "getTaxFee", "setTaxFee" }.Contains(f.Name))
                .Select(f => f.Name);

            Console.WriteLine($"   🎯 Key Functions: {string.Join(", ", keyFunctions)}");
        }
    }
}