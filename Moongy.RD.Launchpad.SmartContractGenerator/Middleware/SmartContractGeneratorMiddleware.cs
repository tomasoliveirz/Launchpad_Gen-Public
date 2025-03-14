using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moongy.RD.Launchpad.ContractGenerator.Contracts.AdvancedFungibleToken.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.Contracts.FungibleToken.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.Contracts.NonFungibleToken.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.Contracts.SemiFungibleToken.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.Tokenomics.AntiWhale.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.Tokenomics.Buyback.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.Tokenomics.Deflation.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.Tokenomics.LiquidityGeneration.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.Tokenomics.Reflections.Interfaces;
using Moongy.RD.Launchpad.ContractGenerator.Tokenomics.Tax.Interfaces;
using Moongy.RD.Launchpad.SmartContractGenerator.Interfaces;
using Moongy.RD.Launchpad.SmartContractGenerator.Utils;

namespace Moongy.RD.Launchpad.SmartContractGenerator.Middleware
{
    public static class SmartContractGeneratorMiddleware
    {
        public static IServiceCollection AddSmartContractGenerator(this IServiceCollection services)
        {
            // Register Tokenomic Compatibility Registry
            services.AddSingleton<ITokenomicCompatibilityRegistry, TokenomicCompatibilityRegistry>();

            // Register Token Generators
            //services.AddSingleton<ISmartContractGenerator, SmartContractGenerator>();
            //services.AddSingleton<IFungibleTokenComposer, FungibleTokenComposer>();
            //services.AddSingleton<ISemiFungibleTokenComposer, SemiFungibleTokenComposer>();
            //services.AddSingleton<IAdvancedFungibleTokenComposer, AdvancedFungibleTokenComposer>();
            //services.AddSingleton<INonFungibleTokenComposer, NonFungibleTokenComposer>();

            // Register Tokenomic Decorators
            //services.AddSingleton<ITaxTokenomicDecorator, TaxTokenomicDecorator>();
            //services.AddSingleton<IDeflationTokenomicDecorator, DeflationTokenomicDecorator>();
            //services.AddSingleton<IBuybackTokenomicDecorator, BuybackTokenomicDecorator>();
            //services.AddSingleton<IReflectionsTokenomicDecorator, ReflectionsTokenomicDecorator>();
            //services.AddSingleton<IAntiWhaleTokenomicDecorator, AntiWhaleTokenomicDecorator>();
            //services.AddSingleton<ILiquidityGenerationTokenomicDecorator, LiquidityGenerationTokenomicDecorator>();

            return services;
        }
    }
}
