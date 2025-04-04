namespace Moongy.RD.Launchpad.Core.Exceptions
{
    public class TokenTaxExceededException : Exception
    {
        public TokenTaxExceededException(double currentTax, double limit)
            : base($"Token tax exceeded the limit. Current: {currentTax}, Limit: {limit}")
        {
        }
    }
}