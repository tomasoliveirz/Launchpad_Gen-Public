using Moongy.RD.Launchpad.Core.Models.Metamodel;
using Moongy.RD.Launchpad.Generator.Contracts.AdvancedFungibleToken.Interfaces;
using Moongy.RD.Launchpad.Generator.Contracts.AdvancedFungibleToken.Models;
using Moongy.RD.Launchpad.Generator.Contracts.Core.Interfaces;
using Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Interfaces;
using Moongy.RD.Launchpad.Generator.Contracts.FungibleToken.Models;
using Moongy.RD.Launchpad.Generator.Contracts.NonFungibleToken.Interfaces;
using Moongy.RD.Launchpad.Generator.Contracts.NonFungibleToken.Models;
using Moongy.RD.Launchpad.Generator.Contracts.SemiFungibleToken.Interfaces;
using Moongy.RD.Launchpad.Generator.Contracts.SemiFungibleToken.Models;
using Moongy.RD.Launchpad.Generator.Tokenomics.AntiWhale.Interfaces;
using Moongy.RD.Launchpad.Generator.Tokenomics.AntiWhale.Models;
using Moongy.RD.Launchpad.Generator.Tokenomics.Buyback.Interfaces;
using Moongy.RD.Launchpad.Generator.Tokenomics.Buyback.Models;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Interfaces;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Validators;
using Moongy.RD.Launchpad.Generator.Tokenomics.Deflation.Interfaces;
using Moongy.RD.Launchpad.Generator.Tokenomics.Deflation.Models;
using Moongy.RD.Launchpad.Generator.Tokenomics.LiquidityGeneration.Interfaces;
using Moongy.RD.Launchpad.Generator.Tokenomics.LiquidityGeneration.Models;
using Moongy.RD.Launchpad.Generator.Tokenomics.Reflections.Interfaces;
using Moongy.RD.Launchpad.Generator.Tokenomics.Reflections.Models;
using Moongy.RD.Launchpad.Generator.Tokenomics.Tax.Interfaces;
using Moongy.RD.Launchpad.Generator.Tokenomics.Tax.Models;
using Moongy.RD.Launchpad.SmartContractGenerator.Enums;
using Moongy.RD.Launchpad.SmartContractGenerator.Interfaces;
using Moongy.RD.Launchpad.SmartContractGenerator.Models;

namespace Moongy.RD.Launchpad.SmartContractGenerator
{

    public class SmartContractGenerator : ISmartContractGenerator
    {
        private readonly ITokenomicCompatibilityRegistry _tokenomicRegistry;
        private readonly IFungibleTokenComposer _fungibleTokenComposer;
        private readonly ISemiFungibleTokenComposer _semiFungibleTokenComposer;
        private readonly IAdvancedFungibleTokenComposer _advancedFungibleTokenComposer;
        private readonly INonFungibleTokenComposer _nonFungibleTokenComposer;
        private readonly ITaxTokenomicDecorator _taxDecorator;
        private readonly IDeflationTokenomicDecorator _deflationDecorator;
        private readonly IBuybackTokenomicDecorator _buybackDecorator;
        private readonly IReflectionsTokenomicDecorator _reflectionDecorator;
        private readonly IAntiWhaleTokenomicDecorator _antiWhaleDecorator;
        private readonly ILiquidityGenerationTokenomicDecorator _liquidityGeneratorDecorator;


        public SmartContractGenerator(
                                        IFungibleTokenComposer fungibleTokenComposer,
                                        ISemiFungibleTokenComposer semiFungibleTokenComposer,
                                        IAdvancedFungibleTokenComposer advancedFungibleTokenComposer,
                                        INonFungibleTokenComposer nonFungibleTokenComposer,
                                        ITaxTokenomicDecorator taxDecorator,
                                        IDeflationTokenomicDecorator deflationDecorator,
                                        IBuybackTokenomicDecorator buybackDecorator,
                                        IReflectionsTokenomicDecorator reflectionDecorator,
                                        IAntiWhaleTokenomicDecorator antiWhaleDecorator,
                                        ILiquidityGenerationTokenomicDecorator liquidityGeneratorDecorator,
                                        ITokenomicCompatibilityRegistry tokenomicRegistry
            )
        {
            SetupRegistry();
            _advancedFungibleTokenComposer = advancedFungibleTokenComposer;
            _fungibleTokenComposer = fungibleTokenComposer;
            _nonFungibleTokenComposer = nonFungibleTokenComposer;
            _semiFungibleTokenComposer = semiFungibleTokenComposer;   
            _taxDecorator = taxDecorator;
            _deflationDecorator = deflationDecorator;
            _reflectionDecorator = reflectionDecorator;
            _buybackDecorator = buybackDecorator;
            _antiWhaleDecorator = antiWhaleDecorator;
            _liquidityGeneratorDecorator = liquidityGeneratorDecorator;
            _tokenomicRegistry = tokenomicRegistry;
        }

        public GenerationResult<FungibleTokenModel> Generate(FungibleTokenModel model, List<ITokenomic> tokenomics, SmartContractVirtualMachine vm)
        {
            TokenomicsValidator.Validate(tokenomics, 20);
            var contractModel = _fungibleTokenComposer.Compose(model);
            contractModel = Decorate<FungibleTokenModel>(tokenomics, contractModel);
            //Generate code
            return new GenerationResult<FungibleTokenModel>();
        }

        public GenerationResult<AdvancedFungibleTokenModel> Generate(AdvancedFungibleTokenModel model, List<ITokenomic> tokenomics, SmartContractVirtualMachine vm)
        {
            TokenomicsValidator.Validate(tokenomics, 20);
            var contractModel = _advancedFungibleTokenComposer.Compose(model);
            contractModel = Decorate<AdvancedFungibleTokenModel>(tokenomics, contractModel);
            //Generate code
            return new GenerationResult<AdvancedFungibleTokenModel>();
        }

        public GenerationResult<NonFungibleTokenModel> Generate(NonFungibleTokenModel model, List<ITokenomic> tokenomics, SmartContractVirtualMachine vm)
        {
            TokenomicsValidator.Validate(tokenomics, 20);
            var contractModel = _nonFungibleTokenComposer.Compose(model);
            contractModel = Decorate<NonFungibleTokenModel>(tokenomics, contractModel);
            //Generate code
            return new GenerationResult<NonFungibleTokenModel>();
        }

        public GenerationResult<SemiFungibleTokenModel> Generate(SemiFungibleTokenModel model, List<ITokenomic> tokenomics, SmartContractVirtualMachine vm)
        {
            TokenomicsValidator.Validate(tokenomics, 20);
            var contractModel = _semiFungibleTokenComposer.Compose(model);
            contractModel = Decorate<SemiFungibleTokenModel>(tokenomics, contractModel);
            //Generate code
            return new GenerationResult<SemiFungibleTokenModel>();
        }

        #region Registry Management
        private void SetupRegistry()
        {
            _tokenomicRegistry.RegisterCompatibility<AdvancedFungibleTokenModel, TaxTokenomicModel>();
            _tokenomicRegistry.RegisterCompatibility<AdvancedFungibleTokenModel, DeflationTokenomicModel>();
            _tokenomicRegistry.RegisterCompatibility<AdvancedFungibleTokenModel, AntiWhaleTokenomicModel>();
            _tokenomicRegistry.RegisterCompatibility<AdvancedFungibleTokenModel, BuybackTokenomicModel>();
            _tokenomicRegistry.RegisterCompatibility<AdvancedFungibleTokenModel, LiquidityGenerationTokenomicModel>();
            _tokenomicRegistry.RegisterCompatibility<AdvancedFungibleTokenModel, ReflectionsTokenomicModel>();

            _tokenomicRegistry.RegisterCompatibility<FungibleTokenModel, TaxTokenomicModel>();
            _tokenomicRegistry.RegisterCompatibility<FungibleTokenModel, DeflationTokenomicModel>();
            _tokenomicRegistry.RegisterCompatibility<FungibleTokenModel, AntiWhaleTokenomicModel>();
            _tokenomicRegistry.RegisterCompatibility<FungibleTokenModel, BuybackTokenomicModel>();
            _tokenomicRegistry.RegisterCompatibility<FungibleTokenModel, LiquidityGenerationTokenomicModel>();
            _tokenomicRegistry.RegisterCompatibility<FungibleTokenModel, ReflectionsTokenomicModel>();

            _tokenomicRegistry.RegisterCompatibility<SemiFungibleTokenModel, TaxTokenomicModel>();
            _tokenomicRegistry.RegisterCompatibility<SemiFungibleTokenModel, DeflationTokenomicModel>();
            _tokenomicRegistry.RegisterCompatibility<SemiFungibleTokenModel, AntiWhaleTokenomicModel>();
            _tokenomicRegistry.RegisterCompatibility<SemiFungibleTokenModel, BuybackTokenomicModel>();
            _tokenomicRegistry.RegisterCompatibility<SemiFungibleTokenModel, LiquidityGenerationTokenomicModel>();
            _tokenomicRegistry.RegisterCompatibility<SemiFungibleTokenModel, ReflectionsTokenomicModel>();

            _tokenomicRegistry.RegisterCompatibility<FungibleTokenModel, DeflationTokenomicModel>();

        }
        #endregion

        #region Tokenomic Decoration

        public SmartContractModel Decorate<TToken>(List<ITokenomic> tokenomics, SmartContractModel model) where TToken : IToken
        {
            foreach (var tokenomic in tokenomics)
            {
                model = Decorate<TToken>(tokenomics, model);
            }
            return model;
        }

        public SmartContractModel Decorate<TToken, TTokenomic, TDecorator>(ITokenomic tokenomic, SmartContractModel model, TDecorator decorator) 
            where TToken : IToken 
            where TTokenomic:ITokenomic
            where TDecorator:ITokenomicDecorator<TTokenomic>
        {
            if(tokenomic is TTokenomic tTokenomic && _tokenomicRegistry.IsCompatible<TToken, TTokenomic>())
            {
                TokenomicsValidator.ValidateIndividualTokenomic(tokenomic);
                return decorator.Decorate(tTokenomic, model);
            }
            return model;
        }

        public SmartContractModel Decorate<TToken>(ITokenomic tokenomic, SmartContractModel model) where TToken : IToken
        {
            model = Decorate<TToken, TaxTokenomicModel, ITaxTokenomicDecorator>(tokenomic, model, _taxDecorator);
            model = Decorate<TToken, DeflationTokenomicModel, IDeflationTokenomicDecorator>(tokenomic, model, _deflationDecorator);
            model = Decorate<TToken, LiquidityGenerationTokenomicModel, ILiquidityGenerationTokenomicDecorator>(tokenomic, model, _liquidityGeneratorDecorator);
            model = Decorate<TToken, AntiWhaleTokenomicModel, IAntiWhaleTokenomicDecorator>(tokenomic, model, _antiWhaleDecorator);
            model = Decorate<TToken, BuybackTokenomicModel, IBuybackTokenomicDecorator>(tokenomic, model, _buybackDecorator);
            model = Decorate<TToken, ReflectionsTokenomicModel, IReflectionsTokenomicDecorator>(tokenomic, model, _reflectionDecorator);
            return model;
        }
        #endregion


        #region Feature extraction
        private IEnumerable<ITokenomic> ExtractTokenomics(object form)
        {

        }

        private IEnumerable<IContractExtension> ExtractExtensions(object form)
        {

        }

        private T ToModel<T>(object t)
        {

        }

        private FungibleTokenModel ToFungibleToken(object t)
        {

        }

        private AdvancedFungibleTokenModel ToAdvancedFungibleToken(object t)
        {

        }

        private NonFungibleTokenModel ToNonFungibleToken(object t)
        {

        }

        private SemiFungibleTokenModel ToSemiFungibleToken(object t)
        {

        }



        #endregion



    }
}
