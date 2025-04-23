namespace Moongy.RD.Launchpad.Tools.TokenWeighter.Exceptions
{
    internal class NoWeightableFeatureFoundException(string name) : Exception
    {
        public string Name { get; set; } = name;
    }
}
