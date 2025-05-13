using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace ScribanSolidityColorizer.Classification
{
    internal static class ClassificationType
    {
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("scriban-solidity.scriban.comment")]
        internal static ClassificationTypeDefinition ScribanComment = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("scriban-solidity.scriban.expression")]
        internal static ClassificationTypeDefinition ScribanExpression = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("scriban-solidity.scriban.number")]
        internal static ClassificationTypeDefinition ScribanNumber = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("scriban-solidity.scriban.string")]
        internal static ClassificationTypeDefinition ScribanString = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("scriban-solidity.scriban.operator")]
        internal static ClassificationTypeDefinition ScribanOperator = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("scriban-solidity.scriban.wrapper")]
        internal static ClassificationTypeDefinition ScribanWrapper = null;



        [Export(typeof(ClassificationTypeDefinition))]
        [Name("scriban-solidity.solidity.comment")]
        internal static ClassificationTypeDefinition SolidityComment = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("scriban-solidity.solidity.data-type")]
        internal static ClassificationTypeDefinition SolidityDataType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("scriban-solidity.solidity.expression")]
        internal static ClassificationTypeDefinition SolidityExpression = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("scriban-solidity.solidity.number")]
        internal static ClassificationTypeDefinition SolidityNumber = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("scriban-solidity.solidity.operator")]
        internal static ClassificationTypeDefinition SolidityOperator = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("scriban-solidity.solidity.string")]
        internal static ClassificationTypeDefinition SolidityString = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("scriban-solidity.solidity.visibility")]
        internal static ClassificationTypeDefinition SolidityVisibility = null;

        [Export]
        [Name("scriban-solidity-description")]
        [BaseDefinition("formal language")]
        internal static ClassificationTypeDefinition ScribanSolidityDescription = null;

        [Export]
        [Name("scriban-solidity-example")]
        [BaseDefinition("formal language")]
        internal static ClassificationTypeDefinition ScribanSolidityExample = null;
    }
}
