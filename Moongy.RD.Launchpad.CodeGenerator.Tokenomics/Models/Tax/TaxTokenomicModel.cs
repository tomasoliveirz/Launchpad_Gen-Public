namespace Moongy.RD.Launchpad.CodeGenerator.Tokenomics.Models.Tax;
public class TaxTokenomicModel
{
    public double TaxFee { get; set; }
    public List<TaxRecipient> TaxRecipients { get; set; } = [];
}
