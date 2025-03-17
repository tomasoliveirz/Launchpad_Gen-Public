namespace Moongy.RD.Launchpad.Core.Models
{
    public class SelectOptions
    {
        public bool IsMandatory { get; set; }
        public List<OptionLabelValue> Options { get; set; } = [];
    }

    public class OptionLabelValue
    {
        public string? Value { get; set; } 
        public string? Label { get; set; }
    }
}
