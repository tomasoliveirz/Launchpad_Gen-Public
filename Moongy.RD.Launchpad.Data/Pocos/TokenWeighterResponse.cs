namespace Moongy.RD.Launchpad.Data.Pocos;

//0-40 - dark green
//41-60 - light green
//61-70 - yellow
//71-80 - dark yellow
//81-90 - orange
//91-... - red
public class TokenWeighterResponse
{
    public decimal TotalWeight { get; set; }
    public List<TokenWeighterItem> FeaturesWeight { get; set; } = [];
}

public class TokenWeighterItem
{
    public string? FeatureName { get; set; }
    public double Weight { get; set; }
}
