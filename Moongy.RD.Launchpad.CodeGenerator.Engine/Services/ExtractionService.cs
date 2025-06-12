using Moongy.RD.Launchpad.CodeGenerator.Engine.Models;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Extractors;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Extractors.Tax;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Extractors;

namespace Moongy.RD.Launchpad.CodeGenerator.Engine.Services
{
    public class ExtractionService : IExtractionService
    {
        private readonly TaxTokenomicExtractor _taxExtractor;
        private readonly AccessControlExtensionExtractor _accessControlExtractor;

        public ExtractionService()
        {
            _taxExtractor = new TaxTokenomicExtractor();
            _accessControlExtractor = new AccessControlExtensionExtractor();
        }

        public async Task<ExtractedModels> ExtractAsync<TForm>(TForm form) where TForm : class
        {
            var standard = FeatureExtractor.GetStandard(form);
            
            var standardTask = Task.Run(() => ExtractStandard(form, standard));
            var tokenomicsTask = Task.Run(() => ExtractTokenomics(form));
            var extensionsTask = Task.Run(() => ExtractExtensions(form));

            // need  to wait for all tasks to complete
            await Task.WhenAll(standardTask, tokenomicsTask, extensionsTask);
            
            return new ExtractedModels
            {
                Standard = standardTask.Result,
                Tokenomics = tokenomicsTask.Result,
                Extensions = extensionsTask.Result
            };
        }

        private object ExtractStandard<TForm>(TForm form, StandardEnum standard) where TForm : class
        {
            return standard switch
            {
                StandardEnum.FungibleToken => FeatureExtractor.FungibleToken.Extract(form),
                StandardEnum.NonFungibleToken => FeatureExtractor.NonFungibleToken.Extract(form),
                _ => throw new NotSupportedException($"Standard {standard} not supported")
            };
        }

        private List<object> ExtractTokenomics<TForm>(TForm form) where TForm : class
        {
            var tokenomics = new List<object>();

            var taxModel = ExtractTax(form);
            if (taxModel != null) tokenomics.Add(taxModel);
            
            return tokenomics;
        }

        private List<object> ExtractExtensions<TForm>(TForm form) where TForm : class
        {
            var extensions = new List<object>();

            var accessControlModel = ExtractAccessControl(form);
            if (accessControlModel != null) extensions.Add(accessControlModel);
            
            return extensions;
        }

        #region Tokenomics Extractors
        private object? ExtractTax<TForm>(TForm form) where TForm : class
        {
            var taxProperty = form.GetType().GetProperty("Tax");
            if (taxProperty?.GetValue(form) is object taxForm)
            {
                return _taxExtractor.Extract(taxForm);
            }
            return null;
        }

        #endregion

        #region Extensions Extractors

        private object? ExtractAccessControl<TForm>(TForm form) where TForm : class
        {
            var accessControlProperty = form.GetType().GetProperty("AccessControl");
            if (accessControlProperty?.GetValue(form) is object accessControlForm)
            {
                return _accessControlExtractor.Extract(accessControlForm);
            }
            return null;
        }

        #endregion
    }
}