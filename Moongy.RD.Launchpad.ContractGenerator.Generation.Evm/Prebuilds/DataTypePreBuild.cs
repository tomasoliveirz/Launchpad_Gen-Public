using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Enums;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.TypeReferences;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Prebuilds
{
    public static class DataTypePreBuild
    {
        public static SimpleTypeReference Uint256 => new SimpleTypeReference(SolidityDataTypeEnum.Uint256);
        public static SimpleTypeReference String => new SimpleTypeReference(SolidityDataTypeEnum.String);
        public static SimpleTypeReference Address => new SimpleTypeReference(SolidityDataTypeEnum.Address);
    }
}
