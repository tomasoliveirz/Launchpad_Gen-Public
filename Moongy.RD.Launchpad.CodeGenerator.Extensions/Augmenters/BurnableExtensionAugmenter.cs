using Moongy.RD.Launchpad.CodeGenerator.Core.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Models;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Generator;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Augmenters
{
    public class BurnableExtensionAugmenter : BaseExtensionAugmenter<BurnExtensionModel>
    {
        public override IReadOnlyCollection<FeatureKind> Requires => Array.Empty<FeatureKind>();
        public override IReadOnlyCollection<FeatureKind> Provides => new[] { FeatureKind.Burnable };

        public override void Augment(ContextMetamodel ctx, BurnExtensionModel model)
        {
            var mod = Main(ctx);
            var burnFunc = new BurnFunction().Build();
            mod.Functions.Add(burnFunc);
        }
    }
}
