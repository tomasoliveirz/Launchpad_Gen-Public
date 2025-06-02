namespace Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Models.BuyBack
{
    public class BuyBackTokenomicModel
    {
        public byte BuyBackFeePercent { get; set; }
        public ulong BuyBackThreshold { get; set; }
        public string? RouterAddress { get; set; }
    }
}
