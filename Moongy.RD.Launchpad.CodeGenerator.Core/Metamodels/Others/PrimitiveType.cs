namespace Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;

public enum PrimitiveType
{
    None,
    // Common
    Bool,
    String,
    Bytes,

    // Solidity-specific
    Address,
    Uint8,
    Uint16,
    Uint32,
    Uint64,
    Uint128,
    Uint256,

    // Rust-specific signed
    Int8,
    Int16,
    Int32,
    Int64,
    Int128,
    // Rust-specific floats
    Float32,
    Float64,
    // Rust-specific
    Char,
    Str,
}
