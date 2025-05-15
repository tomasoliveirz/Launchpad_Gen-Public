namespace Moongy.RD.Launchpad.CodeGenerator.Generation.Evm.Models.Metamodels.Version
{
    public class SoftwareVersion
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Revision { get; set; }

        public int CompareTo(SoftwareVersion? other)
        {
            if (other is null) return 1;
            if (Major != other.Major) return Major.CompareTo(other.Major);
            if (Minor != other.Minor) return Minor.CompareTo(other.Minor);
            return Revision.CompareTo(other.Revision);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not SoftwareVersion other) return false;
            return Major == other.Major
                && Minor == other.Minor
                && Revision == other.Revision;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + Major;
                hash = hash * 31 + Minor;
                hash = hash * 31 + Revision;
                return hash;
            }
        }

        public static bool operator >(SoftwareVersion? left, SoftwareVersion? right)
        {
            if (left is null) return false;
            return left.CompareTo(right) > 0;
        }

        public static bool operator <(SoftwareVersion? left, SoftwareVersion? right)
        {
            if (right is null) return false;
            return left!.CompareTo(right) < 0;
        }

        public static bool operator ==(SoftwareVersion? left, SoftwareVersion? right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;
            return left.Equals(right);
        }

        public static bool operator !=(SoftwareVersion? left, SoftwareVersion? right)
            => !(left == right);

        public static bool operator >=(SoftwareVersion? left, SoftwareVersion? right)
            => left == right || left > right;

        public static bool operator <=(SoftwareVersion? left, SoftwareVersion? right)
            => left == right || left < right;
    }
}
