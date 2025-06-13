using Moongy.RD.Launchpad.CodeGenerator.Engine.Services;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Generators;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Synthesizer;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Augmenters.Tax;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Augmenters.AccessControl;
using Moongy.RD.Launchpad.Data.Forms;
using Moongy.RD.Launchpad.Data.Forms.Extensions;
using Moongy.RD.Launchpad.Data.Forms.Tokenomics;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Augmenters;
using Engine.Services;

namespace Moongy.RD.Launchpad.CodeGenerator.Engine.Test
{
    public static class TestRunner
    {
        public static async Task RunTestAsync()
        {
            Console.WriteLine("full generation test");
            Console.WriteLine("=".PadRight(60, '='));

            try
            {
                var form = CreateTestForm();
                PrintFormDetails(form);

                var engine = CreateEngine();

                Console.WriteLine("\n generating solidity code");
                var solidityCode = await engine.GenerateAsync(form);

                Console.WriteLine("\n done");
                Console.WriteLine("=".PadRight(60, '='));
                Console.WriteLine(solidityCode);
                Console.WriteLine("=".PadRight(60, '='));

                await File.WriteAllTextAsync($"{form.Name}.sol", solidityCode);
                Console.WriteLine($"\n saved to {form.Name}.sol");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n failed: {ex.Message}");
                Console.WriteLine($" Stack trace: {ex.StackTrace}");
            }
        }

        private static FungibleTokenForm CreateTestForm()
        {
            return new FungibleTokenForm
            {
                Name = "TestToken",
                Symbol = "TEST",
                Decimals = 18,
                Premint = 1000000,
                Supply = 10000000,
                Tax = new TaxTokenomic
                {
                    TaxFee = 2.5,
                    Recipients = new List<TaxRecipient>
                    {
                        new TaxRecipient { Address = "0x742d35Cc6635C0532925a3b8D0DE319F0E2a3e15", Share = 60 },
                        new TaxRecipient { Address = "0x8b7395F39b7c9e1e1E1E7FD9E4A2F9C8B7A6D5E4", Share = 40 }
                    }
                },
                AccessControl = new AccessControl
                {
                    Type = AccessControlType.Ownable,
                    Roles = new List<string>()
                },
                HasMinting = true,
                HasBurning = true,
                IsPausable = false,
                HasPermission = false,
                AntiWhaleCap = null,
                RecoveryResponsible = null
            };
        }

        private static CodeGenerationEngine CreateEngine()
        {
            var extractionService = new ExtractionService();
            var compositionService = new CompositionService(new FungibleTokenComposer());
            var augmentationService = new AugmentationService(
                new TaxTokenomicAugmenter(),
                new AccessControlExtensionAugmenter(),
                new BurnableExtensionAugmenter(),
                new MintExtensionAugmenter()
            );
            var synthesizer = new SoliditySynthesizer();
            var generator = new SolidityCodeGenerator();

            return new CodeGenerationEngine(
                extractionService,
                compositionService,
                augmentationService,
                synthesizer,
                generator
            );
        }

        private static void PrintFormDetails(FungibleTokenForm form)
        {
            Console.WriteLine("\n full form:");
            Console.WriteLine($"   Name: {form.Name}");
            Console.WriteLine($"   Symbol: {form.Symbol}");
            Console.WriteLine($"   Decimals: {form.Decimals}");
            Console.WriteLine($"   Premint: {form.Premint:N0}");
            Console.WriteLine($"   Supply: {form.Supply:N0}");
            
            if (form.Tax != null)
            {
                Console.WriteLine($"   Tax: {form.Tax.TaxFee}% with {form.Tax.Recipients.Count} recipients");
                foreach (var recipient in form.Tax.Recipients)
                {
                    Console.WriteLine($"     - {recipient.Address}: {recipient.Share}%");
                }
            }

            if (form.AccessControl != null)
            {
                Console.WriteLine($"   Access Control: {form.AccessControl.Type}");
                if (form.AccessControl.Roles.Any())
                {
                    Console.WriteLine($"   Roles: {string.Join(", ", form.AccessControl.Roles)}");
                }
            }

            Console.WriteLine($"   Features: Mint={form.HasMinting}, Burn={form.HasBurning}");
        }
    }
}