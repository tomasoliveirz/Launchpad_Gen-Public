using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Enums;
using Moongy.RD.Launchpad.SmartContractGenerator.Enums;

namespace Moongy.RD.Launchpad.Tools.TokenWeighter.Models;

public class TokenWeighterRequest
{
    public List<TokenWeighterRequestItem> Features { get; set; } = [];
    public SmartContractVirtualMachine VirtualMachine { get; set; } 
    public double ManualTokenomicWeight { get; set; }
}

public class TokenWeighterRequestItem
{
    public required string Name { get; set; }
    internal bool IsExtension { get; set; }
    internal TokenomicTriggerMode _triggerMode { get; set; }

    internal TokenWeighterRequestItem(bool isExtension, TokenomicTriggerMode triggerMode)
    {
        IsExtension = isExtension;
        _triggerMode = triggerMode;
    }

}

public class TokenWeighterExtensionRequestItem() : TokenWeighterRequestItem(true, TokenomicTriggerMode.None)
{

}


public class TokenWeighterTokenomicRequestItem() : TokenWeighterRequestItem(false, TokenomicTriggerMode.Automatic)
{
    public TokenomicTriggerMode TriggerMode
    {
        get => _triggerMode;
        set => _triggerMode = value;
    }
}