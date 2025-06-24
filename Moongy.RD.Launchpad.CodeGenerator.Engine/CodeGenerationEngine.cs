using Engine.Services;
using Moongy.RD.Launchpad.CodeGenerator.Engine.Interfaces;
using Moongy.RD.Launchpad.CodeGenerator.Engine.Services;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Generators;
using Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Synthesizer;

namespace Moongy.RD.Launchpad.CodeGenerator.Engine
{
    public class CodeGenerationEngine : ICodeGenerationEngine
    {
        private readonly IExtractionService _extractionService;
        private readonly ICompositionService _compositionService;
        private readonly IAugmentationService _augmentationService;
        private readonly SoliditySynthesizer _synthesizer;
        private readonly SolidityCodeGenerator _generator;
        private readonly SlitherAnalyzerService _slitherAnalyzerService;    
        public CodeGenerationEngine(
            IExtractionService extractionService,
            ICompositionService compositionService,
            IAugmentationService augmentationService,
            SoliditySynthesizer synthesizer,
            SolidityCodeGenerator generator,
            SlitherAnalyzerService slitherAnalyzerService
            )
        {
            _extractionService = extractionService;
            _compositionService = compositionService;
            _augmentationService = augmentationService;
            _synthesizer = synthesizer;
            _generator = generator;
            _slitherAnalyzerService = slitherAnalyzerService;
        }

        public async Task<string> GenerateAsync<TForm>(TForm form) where TForm : class
        {
            //extract models from the form
            var extractedModels = await _extractionService.ExtractAsync(form);
             // compose the models into a context metamodel
            var moduleFile = await _compositionService.ComposeAsync(extractedModels);
            // compose tokenomics and extensions 
            await _augmentationService.AugmentAsync(moduleFile, extractedModels);
            // synthesize the context metamodel into a solidity model
            var solidityModel = _synthesizer.Synthesize(moduleFile);
            // generate the solidity code from the solidity model
            var solidityFile = _generator.Generate(solidityModel); 
            // Compile solidity code and check for errors
            var slitherReport = await _slitherAnalyzerService.AnalyzeAsync(solidityFile);
            Console.WriteLine(slitherReport);

            return solidityFile;
        }
    }
}