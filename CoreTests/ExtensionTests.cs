using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Attributes;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Standards.Models;
using Moongy.RD.Launchpad.CodeGenerator.Standards.ExtensionMethods;

namespace CoreTests
{
    [Standard(Source = StandardEnum.FungibleToken)]
    public class A
    {
        [StandardProperty(Source = StandardEnum.FungibleToken, Name = nameof(FungibleTokenModel.MaxSupply))]
        public ulong MaxSupply { get; set; }

        public B B { get; set; }
    }

    public class  B 
    {
        [StandardProperty(Source = StandardEnum.FungibleToken, Name = nameof(FungibleTokenModel.Premint))]
        public ulong Premint { get; set; }
    }

    [TestClass]
    public sealed class ExtensionTests
    {
        [TestMethod]
        public void RecursivePropertyFetch()
        {
            A a = new A() { B = new B() { Premint = 2}, MaxSupply = 12 };
            var properties = typeof(A).GetStandardPropertiesRecursive(a);
            Assert.AreEqual(2, properties.Count());
        }
    }
}
