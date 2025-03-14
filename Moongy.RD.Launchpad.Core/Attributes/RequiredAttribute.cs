namespace Moongy.RD.Launchpad.Core.Attributes;
public class RequiredAttribute : ValidationAttribute
{
    public override void Validate(object value)
    {
            if (value != null) return;
            //CONTINUE
    }
}
