namespace Moongy.RD.Launchpad.Core.Exceptions
{
    public class TokenWeightExceededException : Exception
    {
        public TokenWeightExceededException(double currentWeight, double limit)
            : base($"Token weight exceeded the limit. Current: {currentWeight}, Limit: {limit}")
        {
        }
    }
}