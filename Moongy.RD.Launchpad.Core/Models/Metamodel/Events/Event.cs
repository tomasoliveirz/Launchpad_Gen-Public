namespace Moongy.RD.Launchpad.Core.Models.Metamodel.Events
{
    public class Event
    {
        public string Name { get; set; } = "";
        public List<Argument> Arguments { get; set; }
    }
}
