

using Moongy.RD.Launchpad.CodeGenerator.Core.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Models;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Composers.Generator;

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Augmenters
{
    public class MintExtensionAugmenter : BaseExtensionAugmenter<MintExtensionModel>
    {
        public override IReadOnlyCollection<FeatureKind> Requires => Array.Empty<FeatureKind>();
        public override IReadOnlyCollection<FeatureKind> Provides => new[] { FeatureKind.Mintable };

        public override void Augment(ContextMetamodel ctx, MintExtensionModel model)
        {
            var mod = Main(ctx);

            var mintFunc = new MintFunction().Build();
            mod.Functions.Add(mintFunc);
        }
    }
}
