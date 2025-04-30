using System.Linq;
using System.Reflection;
using ScribanSolidityColorizer.Attributes;
using ScribanSolidityColorizer.Enums;

namespace ScribanSolidityColorizer.Expressions
{
    public static class SolidityExpressions
    {
        // Control Keywords
        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityKeyword)]
        public const string Contract = "contract";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityKeyword)]
        public const string Function = "function";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityKeyword)]
        public const string Modifier = "modifier";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityKeyword)]
        public const string Struct = "struct";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityKeyword)]
        public const string Enum = "enum";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityKeyword)]
        public const string Event = "event";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityKeyword)]
        public const string Error = "error";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityKeyword)]
        public const string Mapping = "mapping";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityKeyword)]
        public const string Require = "require";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityKeyword)]
        public const string Revert = "revert";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityKeyword)]
        public const string Emit = "emit";

        // Data Types
        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType)]
        public const string Address = "address";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType)]
        public const string Uint = "uint";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType)]
        public const string Uint8 = "uint8";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType)]
        public const string Uint16 = "uint16";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType)]
        public const string Uint32 = "uint32";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType)]
        public const string Uint64 = "uint64";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType)]
        public const string Uint128 = "uint128";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType)]
        public const string Uint256 = "uint256";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType)]
        public const string Int = "int";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType)]
        public const string Int8 = "int8";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType)]
        public const string Int16 = "int16";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType)]
        public const string Int32 = "int32";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType)]
        public const string Int64 = "int64";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType)]
        public const string Int128 = "int128";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType)]
        public const string Int256 = "int256";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType)]
        public const string Bool = "bool";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType)]
        public const string String = "string";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType)]
        public const string Bytes = "bytes";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType)]
        public const string Bytes32 = "bytes32";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType)]
        public const string Fixed = "fixed";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType)]
        public const string Ufixed = "ufixed";

        // Visibility / Mutability
        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityVisibility)]
        public const string Public = "public";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityVisibility)]
        public const string Private = "private";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityVisibility)]
        public const string Internal = "internal";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityVisibility)]
        public const string External = "external";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityVisibility)]
        public const string View = "view";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityVisibility)]
        public const string Pure = "pure";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityVisibility)]
        public const string Payable = "payable";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityVisibility)]
        public const string Constant = "constant";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityVisibility)]
        public const string Immutable = "immutable";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityVisibility)]
        public const string Override = "override";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityVisibility)]
        public const string Virtual = "virtual";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityVisibility)]
        public const string Calldata = "calldata";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityVisibility)]
        public const string Memory = "memory";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityVisibility)]
        public const string Storage = "storage";

        // Value Literals
        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityValue)]
        public const string True = "true";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityValue)]
        public const string False = "false";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityValue)]
        public const string This = "this";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityValue)]
        public const string Super = "super";

        // Automatic grouped collection
        public static readonly string[] All = GetAllConstants();

        private static string[] GetAllConstants()
        {
            return typeof(SolidityExpressions)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
                .Select(fi => (string)fi.GetRawConstantValue())
                .ToArray();
        }
    }
}
