namespace Moongy.RD.Launchpad.Core.Exceptions
{
    public class TokenWeightExceededException(double currentWeight, double limit) : Exception($"Token weight exceeded the limit. Current: {currentWeight}, Limit: {limit}")
    {
    }
}