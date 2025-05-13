using System.Linq;
using System.Reflection;
using ScribanSolidityColorizer.Attributes;
using ScribanSolidityColorizer.Enums;

namespace ScribanSolidityColorizer.Expressions
{
    public static class SolidityExpressions
    {
        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityExpression,
        Description = "Imports external files or contracts.",
        UsageExample = "import {A, B} from \"./MyFile.sol\";")]
        public const string Import = "import";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityExpression,
        Description = "Specifies compiler version and settings.",
        UsageExample = "pragma solidity ^0.8.0;")]
        public const string Pragma = "pragma";


        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityExpression,
        Description = "",
        UsageExample = "")]
        public const string From = "from";


        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityExpression,
        Description = "",
        UsageExample = "")]
        public const string Is = "is";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityExpression,
        Description = "",
        UsageExample = "")]
        public const string CloseBlock = "}";


        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityExpression,
        Description = "",
        UsageExample = "")]
        public const string OpenBlock = "{";


        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityExpression,
            Description = "Specifies Solidity language version.",
            UsageExample = "pragma solidity ^0.8.0;")]
        public const string Solidity = "solidity";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityExpression,
            Description = "Defines a contract structure.",
            UsageExample = "contract MyContract { ... }")]
        public const string Contract = "contract";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityExpression,
            Description = "Defines a function in a contract.",
            UsageExample = "function transfer(address to, uint amount) public { ... }")]
        public const string Function = "function";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityExpression,
    Description = "Declares a constructor, which runs once when the contract is deployed and sets initial state.",
    UsageExample = "constructor(address initialOwner) { owner = initialOwner; }")]
        public const string Constructor = "constructor";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityExpression,
    Description = "Defines a fallback function, executed on calls with no data or unmatched function signatures.",
    UsageExample = "fallback() external payable { /* handle plain ether transfers */ }")]
        public const string Fallback = "fallback";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityExpression,
            Description = "Defines a receive function, specifically for handling plain Ether transfers.",
            UsageExample = "receive() external payable { /* handle ether reception */ }")]
        public const string Receive = "receive";


        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityExpression,
            Description = "Defines a function modifier.",
            UsageExample = "modifier onlyOwner { require(msg.sender == owner); _; }")]
        public const string Modifier = "modifier";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityExpression,
            Description = "Defines a structured data type.",
            UsageExample = "struct User { string name; uint balance; }")]
        public const string Struct = "struct";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityExpression,
            Description = "Defines an enumeration type.",
            UsageExample = "enum Status { Pending, Shipped, Delivered }")]
        public const string Enum = "enum";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityExpression,
            Description = "Defines an event for logging on the blockchain.",
            UsageExample = "event Transfer(address from, address to, uint value);")]
        public const string Event = "event";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityExpression,
            Description = "Defines a custom error type.",
            UsageExample = "error Unauthorized(address caller);")]
        public const string Error = "error";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityExpression,
            Description = "Declares a mapping (key-value storage).",
            UsageExample = "mapping(address => uint) balances;")]
        public const string Mapping = "mapping";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityExpression,
      Description = "Asserts a condition; reverts if false.",
      UsageExample = "require(balance >= amount, \"Insufficient balance\");")]
        public const string Require = "require";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityExpression,
            Description = "Forces transaction failure and reverts state.",
            UsageExample = "revert(\"Unauthorized\");")]
        public const string Revert = "revert";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityExpression,
            Description = "Triggers an event.",
            UsageExample = "emit Transfer(msg.sender, recipient, amount);")]
        public const string Emit = "emit";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType,
    Description = "Ethereum address type (20 bytes).",
    UsageExample = "address public owner;")]
        public const string Address = "address";


        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType,
            Description = "Unsigned integer (default 256 bits).",
            UsageExample = "uint balance;")]
        public const string Uint = "uint";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType,
            Description = "Unsigned 8-bit integer.",
            UsageExample = "uint8 smallValue;")]
        public const string Uint8 = "uint8";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType,
            Description = "Unsigned 16-bit integer.",
            UsageExample = "uint16 mediumValue;")]
        public const string Uint16 = "uint16";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType,
            Description = "Unsigned 32-bit integer.",
            UsageExample = "uint32 id;")]
        public const string Uint32 = "uint32";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType,
            Description = "Unsigned 64-bit integer.",
            UsageExample = "uint64 bigId;")]
        public const string Uint64 = "uint64";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType,
            Description = "Unsigned 128-bit integer.",
            UsageExample = "uint128 veryBigId;")]
        public const string Uint128 = "uint128";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType,
            Description = "Unsigned 256-bit integer.",
            UsageExample = "uint256 hugeValue;")]
        public const string Uint256 = "uint256";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType,
            Description = "Signed integer (default 256 bits).",
            UsageExample = "int balance;")]
        public const string Int = "int";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType,
            Description = "signed 8-bit integer.",
            UsageExample = "int8 smallValue;")]
        public const string Int8 = "int8";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType,
            Description = "Signed 16-bit integer.",
            UsageExample = "int16 mediumValue;")]
        public const string Int16 = "int16";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType,
            Description = "Signed 32-bit integer.",
            UsageExample = "int32 id;")]
        public const string Int32 = "int32";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType,
            Description = "Signed 64-bit integer.",
            UsageExample = "int64 bigId;")]
        public const string Int64 = "int64";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType,
            Description = "Signed 128-bit integer.",
            UsageExample = "int128 veryBigId;")]
        public const string Int128 = "int128";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType,
            Description = "Signed 256-bit integer.",
            UsageExample = "int256 hugeValue;")]
        public const string Int256 = "int256";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType,
        Description = "Boolean value (true/false).",
        UsageExample = "bool isActive;")]
        public const string Bool = "bool";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType,
        Description = "UTF-8 encoded string.",
        UsageExample = "string public name;")]
        public const string String = "string";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType,
    Description = "Dynamic byte array.",
    UsageExample = "bytes public data;")]
        public const string Bytes = "bytes";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType,
    Description = "Fixed-size 32-byte array.",
    UsageExample = "bytes32 public hash;")]
        public const string Bytes32 = "bytes32";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType,
    Description = "Fixed-point decimal (negative allowed).",
    UsageExample = "fixed public price;")]
        public const string Fixed = "fixed";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityDataType,
    Description = "Fixed-point decimal (unsigned).",
    UsageExample = "ufixed public rate;")]
        public const string Ufixed = "ufixed";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityVisibility,
     Description = "Publicly accessible.",
     UsageExample = "function deposit() public { ... }")]
        public const string Public = "public";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityVisibility,
            Description = "Accessible only within contract.",
            UsageExample = "function _internal() private { ... }")]
        public const string Private = "private";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityVisibility,
            Description = "Accessible within contract or derived.",
            UsageExample = "function helper() internal { ... }")]
        public const string Internal = "internal";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityVisibility,
            Description = "Accessible externally via message calls.",
            UsageExample = "function callMe() external { ... }")]
        public const string External = "external";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityVisibility,
            Description = "Does not modify contract state.",
            UsageExample = "function viewData() public view returns (uint) { ... }")]
        public const string View = "view";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityVisibility,
            Description = "Does not read or modify state.",
            UsageExample = "function compute() public pure returns (uint) { ... }")]
        public const string Pure = "pure";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityVisibility,
            Description = "Can receive Ether.",
            UsageExample = "function buy() public payable { ... }")]
        public const string Payable = "payable";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityVisibility,
            Description = "Value is constant.",
            UsageExample = "uint public constant MAX_SUPPLY = 1000;")]
        public const string Constant = "constant";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityVisibility,
    Description = "Value set once during construction.",
    UsageExample = "uint immutable createdAt;")]
        public const string Immutable = "immutable";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityVisibility,
            Description = "Overrides a parent contract’s method.",
            UsageExample = "function doSomething() public override { ... }")]
        public const string Override = "override";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityVisibility,
            Description = "Allows further overrides in child contracts.",
            UsageExample = "function customizable() public virtual { ... }")]
        public const string Virtual = "virtual";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityVisibility,
            Description = "Indicates function parameters live in calldata.",
            UsageExample = "function process(bytes calldata input) external { ... }")]
        public const string Calldata = "calldata";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityVisibility,
            Description = "Indicates variable is in memory (temporary).",
            UsageExample = "function storeInMemory() public { uint ; }")]
        public const string Memory = "memory";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityVisibility,
            Description = "Indicates variable is in contract storage.",
            UsageExample = "uint[] storage data = myArray;")]
        public const string Storage = "storage";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityExpression,
            Description = "Boolean true value.",
            UsageExample = "bool flag = true;")]
        public const string True = "true";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityExpression,
            Description = "Boolean false value.",
            UsageExample = "bool flag = false;")]
        public const string False = "false";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityExpression,
            Description = "Refers to current contract instance.",
            UsageExample = "this.someFunction();")]
        public const string This = "this";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityExpression,
            Description = "Refers to immediate parent contract.",
            UsageExample = "super.someFunction();")]
        public const string Super = "super";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityOperator,
    Description = "Equality comparison operator.",
    UsageExample = "if (a == b) { ... }")]
        public const string Equal = "==";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityOperator,
            Description = "Inequality comparison operator.",
            UsageExample = "if (a != b) { ... }")]
        public const string NotEqual = "!=";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityOperator,
            Description = "Less than comparison operator.",
            UsageExample = "if (a < b) { ... }")]
        public const string LessThan = "<";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityOperator,
            Description = "Greater than comparison operator.",
            UsageExample = "if (a > b) { ... }")]
        public const string GreaterThan = ">";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityOperator,
            Description = "Less than or equal comparison operator.",
            UsageExample = "if (a <= b) { ... }")]
        public const string LessThanOrEqual = "<=";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityOperator,
            Description = "Greater than or equal comparison operator.",
            UsageExample = "if (a >= b) { ... }")]
        public const string GreaterThanOrEqual = ">=";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityOperator,
            Description = "Logical AND operator.",
            UsageExample = "if (a && b) { ... }")]
        public const string And = "&&";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityOperator,
            Description = "Logical OR operator.",
            UsageExample = "if (a || b) { ... }")]
        public const string Or = "||";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityOperator,
            Description = "Logical NOT operator.",
            UsageExample = "if (!a) { ... }")]
        public const string Not = "!";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityOperator,
            Description = "Addition operator.",
            UsageExample = "uint c = a + b;")]
        public const string Plus = "+";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityOperator,
            Description = "Subtraction operator.",
            UsageExample = "uint c = a - b;")]
        public const string Minus = "-";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityOperator,
            Description = "Multiplication operator.",
            UsageExample = "uint c = a * b;")]
        public const string Multiply = "*";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityOperator,
            Description = "Division operator.",
            UsageExample = "uint c = a / b;")]
        public const string Divide = "/";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityOperator,
            Description = "Modulo (remainder) operator.",
            UsageExample = "uint c = a % b;")]
        public const string Modulo = "%";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityOperator,
    Description = "Assignment operator.",
    UsageExample = "a = b;")]
        public const string Assign = "=";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityOperator,
            Description = "Addition assignment operator.",
            UsageExample = "a += b;")]
        public const string PlusAssign = "+=";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityOperator,
            Description = "Subtraction assignment operator.",
            UsageExample = "a -= b;")]
        public const string MinusAssign = "-=";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityOperator,
            Description = "Multiplication assignment operator.",
            UsageExample = "a *= b;")]
        public const string MultiplyAssign = "*=";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityOperator,
            Description = "Division assignment operator.",
            UsageExample = "a /= b;")]
        public const string DivideAssign = "/=";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityOperator,
            Description = "Modulo assignment operator.",
            UsageExample = "a %= b;")]
        public const string ModuloAssign = "%=";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityOperator,
            Description = "Bitwise AND assignment operator.",
            UsageExample = "a &= b;")]
        public const string AndAssign = "&=";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityOperator,
            Description = "Bitwise OR assignment operator.",
            UsageExample = "a |= b;")]
        public const string OrAssign = "|=";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityOperator,
            Description = "Bitwise XOR assignment operator.",
            UsageExample = "a ^= b;")]
        public const string XorAssign = "^=";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityOperator,
            Description = "Left shift assignment operator.",
            UsageExample = "a <<= b;")]
        public const string LeftShiftAssign = "<<=";

        [ProgrammingLanguageExpression(ScribanSolidityTokenTypes.SolidityOperator,
            Description = "Right shift assignment operator.",
            UsageExample = "a >>= b;")]
        public const string RightShiftAssign = ">>=";

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
