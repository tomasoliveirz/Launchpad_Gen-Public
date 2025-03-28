namespace Moongy.RD.Launchpad.Core.Models.Metamodel.Events
{
    public class Event
    {
        public string Name { get; set; } = "";
        public List<EventArgument> Arguments { get; set; }
    }
}
