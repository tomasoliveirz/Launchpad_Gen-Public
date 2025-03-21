namespace Moongy.RD.Launchpad.Core.Attributes
{
    public class OptionLabelAttribute : Attribute
    {
        public string? Label { get; set; }
        public string? Description { get; set; }
        public bool Ignore { get; set; }
    }
}
