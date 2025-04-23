using System.Reflection;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Attributes;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Models;

namespace Moongy.RD.Launchpad.Generator.Tokenomics.Core.Support
{
    public static class Tokenomic
    {
        public static double GetTokenomicWeight(BaseTokenomicModel model)
        {
            var type = model.GetType();
            return GetTokenomicWeight(type);
        }

        public static double GetTokenomicWeight(Type type)
        {
            var attribute = type.GetCustomAttribute<TokenomicAttribute>(false);
            if (attribute == null) return 0;
            return attribute.Weight;
        }
    }
}
