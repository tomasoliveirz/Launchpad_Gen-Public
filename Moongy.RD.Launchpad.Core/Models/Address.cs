namespace Moongy.RD.Launchpad.Core.Models
{
    public class Address(string address)
    {
        private readonly string _address = address;

        public override string ToString()
        {
            return _address;
        }

        public static bool IsNullOrEmpty(Address? address)
        {
            return address == null || string.IsNullOrEmpty(address.ToString());
        }

        public static implicit operator string(Address address)
        {
            return address?._address??"";
        }
    }
}
