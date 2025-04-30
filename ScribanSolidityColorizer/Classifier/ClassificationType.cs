using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace ScribanSolidityColorizer.Classification
{
    internal static class ClassificationType
    {
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("scriban-solidity.solidity.keyword")]
        internal static ClassificationTypeDefinition SolidityKeyword = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("scriban-solidity.solidity.data-type")]
        internal static ClassificationTypeDefinition SolidityType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("scriban-solidity.solidity.comment")]
        internal static ClassificationTypeDefinition SolidityComment = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("scriban-solidity.solidity.string-literal")]
        internal static ClassificationTypeDefinition StringLiteral = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("scriban-solidity.solidity.number-literal")]
        internal static ClassificationTypeDefinition NumberLiteral = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("scriban-solidity.solidity.value")]
        internal static ClassificationTypeDefinition SolidityValue = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("scriban-solidity.solidity.visibility")]
        internal static ClassificationTypeDefinition SolidityVisibility = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("scriban-solidity.scribans.expression")]
        internal static ClassificationTypeDefinition TemplateExpression = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("scriban-solidity.scribans.control")]
        internal static ClassificationTypeDefinition TemplateControl = null;
    }
}
