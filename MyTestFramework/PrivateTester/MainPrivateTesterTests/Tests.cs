using Core.PrivateTester;
using Xunit;

namespace Tests.PrivateTester.MainPrivateTesterTests
{
    public class Tests
    {
        private Core.IPrivateTester privateTester;

        [Fact]
        public void Throw_if_member_with_given_name_not_presnt()
        {
            //Arrange
            object @object = new PrivateFieldClass(10);
            privateTester = new Core.PrivateTester<PrivateFieldClass>();

            //Assert
            var fieldName = "nonExistingField";
            Assert.Throws<PrivatePropertyException>(
                () => privateTester.SetPrivateField(@object, fieldName, 0)
                );
        }

        [Fact]
        public void Change_value_of_private_field()
        {
            //Arrange
            var instance = new PrivateFieldClass(10);
            privateTester = new Core.PrivateTester<PrivateFieldClass>();

            var currentPrivateIntValue = instance.GetPrivateInt();
            Assert.Equal(10, currentPrivateIntValue);

            //Act
            privateTester.SetPrivateField(instance, "privateInt", 12);

            //Assert
            currentPrivateIntValue = instance.GetPrivateInt();
            Assert.Equal(12, currentPrivateIntValue);
        }

        [Fact]
        public void Change_value_of_private_string()
        {
            //Arrange
            var instance = new PrivateFieldClass(10);
            privateTester = new Core.PrivateTester<PrivateFieldClass>();

            var currentPrivateStringValue = instance.GetPrivateString();
            Assert.Equal("nothing", currentPrivateStringValue);

            //Act
            privateTester.SetPrivateField(instance, "privateString", "value");

            //Assert
            currentPrivateStringValue = instance.GetPrivateString();
            Assert.Equal("value", currentPrivateStringValue);
        }

        [Fact]
        public void Throw_if_member_with_given_is_public()
        {
            //Arrange
            object @object = new PrivateFieldClass(10);
            privateTester = new Core.PrivateTester<PrivateFieldClass>();

            //Assert
            var fieldName = "publicInt";
            Assert.Throws<PrivatePropertyException>(
                () => privateTester.SetPrivateField(@object, fieldName, 0)
                );
        }

        [Fact]
        public void Throw_if_invlaid_type_of_parameter_provided()
        {
            //Arrange
            object @object = new PrivateFieldClass(10);
            privateTester = new Core.PrivateTester<PrivateFieldClass>();

            //Assert
            var fieldName = "privateInt";
            Assert.Throws<PrivatePropertyException>(
                () => privateTester.SetPrivateField(@object, fieldName, "zero")
                );
        }

        private class PrivateFieldClass
        {
            private int privateInt;
            private string privateString;

            public int publicInt;

            public PrivateFieldClass(int privateInt)
            {
                this.privateInt = privateInt;
                privateString = "nothing";
            }

            public int GetPrivateInt()
            {
                return privateInt;
            }
            public string GetPrivateString()
            {
                return privateString;
            }
        }
    }
}
