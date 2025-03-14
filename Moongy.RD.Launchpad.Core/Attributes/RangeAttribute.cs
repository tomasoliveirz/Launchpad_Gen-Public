namespace Moongy.RD.Launchpad.Core.Attributes
{
    public class RangeAttribute : ValidationAttribute
    {
        public int Maximum { get; set; }
        public int Minimum { get; set; }
        public string? Name { get; set; }

        public override void Validate(object? value)
        {
            if (value == null) return;
            if (value is int iValue) Validate(iValue);
        }

        internal static void Validate(decimal value)
        {
            //CONTINUE
        }

    }
}
