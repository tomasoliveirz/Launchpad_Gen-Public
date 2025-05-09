using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Errors;
using Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Models.Metamodels.Parameters;

namespace Moongy.RD.Launchpad.ContractGenerator.Generation.Evm.Prebuilds
{
    public static class PermitPrebuild
    {

        private static List<ErrorParameterModel> _eRC2612ExpiredSignatureParameters = [new() { Name = "deadline", Type = DataTypePreBuild.Uint256 }];
        public static ErrorModel ERC2612ExpiredSignature => new ErrorModel() { Name = "ERC2612ExpiredSignature", Parameters = _eRC2612ExpiredSignatureParameters };

        private static List<ErrorParameterModel> _eRC2612InvalidSignerParameters = [
            new() { Name = "signer", Type = DataTypePreBuild.Address },
            new() { Name = "owner", Type = DataTypePreBuild.Address }];

        public static ErrorModel ERC2612InvalidSigner => new ErrorModel() { Name = "ERC2612ExpiredSignature", Parameters = _eRC2612InvalidSignerParameters };


    }
}
