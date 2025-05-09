namespace Moongy.RD.Launchpad.Core.Exceptions
{
    public class TokenTaxExceededException(double currentTax, double limit) : Exception($"Token tax exceeded the limit. Current: {currentTax}, Limit: {limit}")
    {
    }
}