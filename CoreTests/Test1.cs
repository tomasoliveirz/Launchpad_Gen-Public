using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Core.Validators;

namespace CoreTests
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestEnumerable()
        {
            var @enum1 = new EnumDefinition() { Name = "A", Members = [] };
            var @enum2 = new EnumDefinition() { Name = "A", Members = { "1" } };
            var val = new LaunchpadValidator<EnumDefinition>();

            try
            {
                val.Validate(enum2);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
            Assert.ThrowsException<ValidationException>(() => val.Validate(enum1));
        }

        [TestMethod]
        public void TestTypeReference()
        {
            var ts1 = new TypeReference() { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.Bool };
            var ts2 = new TypeReference() { Kind = TypeReferenceKind.Simple, Primitive = PrimitiveType.None };

            var ta1 = new TypeReference() { Kind = TypeReferenceKind.Array, ElementType = ts1 };
            var ta2 = new TypeReference() { Kind = TypeReferenceKind.Array };

            var tm1 = new TypeReference() { Kind = TypeReferenceKind.Mapping, KeyType = ts1, ValueType = ta1 };
            var tm2 = new TypeReference() { Kind = TypeReferenceKind.Mapping, KeyType = ts1 };
            var tm3 = new TypeReference() { Kind = TypeReferenceKind.Mapping };
            var tm4 = new TypeReference() { Kind = TypeReferenceKind.Mapping, ValueType = ta1 };

            var tt1 = new TypeReference() { Kind = TypeReferenceKind.Tuple, ElementTypes = [ta1, tm1, ts1] };
            var tt2 = new TypeReference() { Kind = TypeReferenceKind.Tuple, ElementTypes = [ta1] };

            var tc1 = new TypeReference() { Kind = TypeReferenceKind.Custom, TypeName = "A" };
            var tc2 = new TypeReference() { Kind = TypeReferenceKind.Custom, TypeName = "" };
            var tc3 = new TypeReference() { Kind = TypeReferenceKind.Custom };

            var val = new TypeReferenceValidator();

            Assert.ThrowsException<ValidationException>(() => val.Validate(ts2));
            Assert.ThrowsException<ValidationException>(() => val.Validate(ta2));
            Assert.ThrowsException<ValidationException>(() => val.Validate(tm2));
            Assert.ThrowsException<ValidationException>(() => val.Validate(tm3));
            Assert.ThrowsException<ValidationException>(() => val.Validate(tm4));
            Assert.ThrowsException<ValidationException>(() => val.Validate(tt2));
            Assert.ThrowsException<ValidationException>(() => val.Validate(tc2));
            Assert.ThrowsException<ValidationException>(() => val.Validate(tc3));

            try
            {
                val.Validate(ts1);
                val.Validate(ta1);
                val.Validate(tm1);
                val.Validate(tt1);
                val.Validate(tc1);
            }
            catch (ValidationException ex) 
            { 
                Assert.Fail(ex.Message);
            }
        }
    
    
    }
}
