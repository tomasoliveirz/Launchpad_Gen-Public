using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.TypeReferences;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Prebuilds
{
    public static class DataTypePreBuild
    {
        public static SimpleTypeReference Uint256 => new SimpleTypeReference(SolidityDataTypeEnum.Uint256);
        public static SimpleTypeReference String => new SimpleTypeReference(SolidityDataTypeEnum.String);
        public static SimpleTypeReference Address => new SimpleTypeReference(SolidityDataTypeEnum.Address);
        public static SimpleTypeReference Bytes32 => new SimpleTypeReference(SolidityDataTypeEnum.Bytes32);
        public static SimpleTypeReference Uint8 => new SimpleTypeReference(SolidityDataTypeEnum.Uint8);
        public static SimpleTypeReference Bytes4 => new SimpleTypeReference(SolidityDataTypeEnum.Bytes4);
        public static SimpleTypeReference Bool => new SimpleTypeReference(SolidityDataTypeEnum.Bool);
        public static SimpleTypeReference Bytes => new SimpleTypeReference(SolidityDataTypeEnum.Bytes);
    }
}
