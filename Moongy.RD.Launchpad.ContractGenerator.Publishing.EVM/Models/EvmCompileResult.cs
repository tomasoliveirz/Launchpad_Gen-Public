namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.EVM.Models
{
    public class EvmCompileResult
    {
        public bool Success { get; set; }
        public string? Bytecode { get; set; }
        public string? Abi { get; set; }
        public string? Errors { get; set; }
    }

}
