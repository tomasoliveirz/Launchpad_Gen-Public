using System.Reflection;
using Microsoft.Extensions.DependencyModel;
using Moongy.RD.Launchpad.Generator.Extensions.Core.Attributes;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Attributes;
using Moongy.RD.Launchpad.Generator.Tokenomics.Core.Enums;
using Moongy.RD.Launchpad.Tools.TokenWeighter.Exceptions;
using Moongy.RD.Launchpad.Tools.TokenWeighter.ExtensionMethods;
using Moongy.RD.Launchpad.Tools.TokenWeighter.Models;

namespace Moongy.RD.Launchpad.Tools.TokenWeighter
{
    public class TokenWeighter : ITokenWeighter
    {
        private static ContractExtensionAttribute? GetContractExtensionAttribute(IEnumerable<Type> types, string name) 
        {
            var match = types.FirstOrDefault(t => string.Equals(t.Name, name, StringComparison.OrdinalIgnoreCase));
            return match?.GetCustomAttribute<ContractExtensionAttribute>(false);
        }

        private static TokenomicAttribute? GetTokenomicAttribute(IEnumerable<Type> types, string name) 
        {
            var match = types.FirstOrDefault(t => string.Equals(t.Name, name, StringComparison.OrdinalIgnoreCase));
            return match?.GetCustomAttribute<TokenomicAttribute>(false);
        }


        private static IEnumerable<Type> WeightableFeatures
        {
            get
            {
                var result = new List<Type>();
                var deps = (DependencyContext.Default?.CompileLibraries) ?? throw new NoWeightableFeaturesFoundException();
                foreach (var lib in deps)
                {
                    Assembly assembly;
                    try
                    {
                        assembly = Assembly.Load(new AssemblyName(lib.Name));
                    }
                    catch
                    {
                        continue;
                    }

                    Type[] types;
                    try
                    {
                        types = assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException ex)
                    {
                        types = ex.Types.Where(t => t != null && t.IsWeightable()).OfType<Type>().ToArray();
                    }

                    foreach (var t in types)
                    {
                        if (t.IsClass && !t.IsAbstract && t.IsPublic)
                            result.Add(t);
                    }
                }
                return result;
            }
        }

        public TokenWeighterResponse GetWeight(TokenWeighterRequest request)
        {
            var weightableFeatures = WeightableFeatures;
            var result = new TokenWeighterResponse() { FeaturesWeight = new() };
            foreach(var item in request.Features)
            {
                double weight = 0;
                if (item.IsExtension)
                {
                    var feature = GetContractExtensionAttribute(weightableFeatures, item.Name);
                    if (feature == null) throw new NoWeightableFeatureFoundException(item.Name);
                    weight = feature.Weight;
                }
                else
                {
                    var feature = GetTokenomicAttribute(weightableFeatures, item.Name);
                    if (feature == null) throw new NoWeightableFeatureFoundException(item.Name);
                    weight = item._triggerMode == TokenomicTriggerMode.Manual ? request.ManualTokenomicWeight : feature.Weight;
                }
                result.FeaturesWeight.Add(new() { FeatureName = item.Name, Weight = weight });
            }
            return result;
        }
    }
}
