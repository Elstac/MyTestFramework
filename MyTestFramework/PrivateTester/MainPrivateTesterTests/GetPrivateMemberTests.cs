using Core.PrivateTester;
using Xunit;

namespace Tests.PrivateTester.MainPrivateTesterTests
{
    public class GetPrivateMemberTests
    {
        private Core.IPrivateTester privateTester;

        [Fact]
        public void Throws_if_property_not_present()
        {
            //Arrange
            object @object = new PrivateFieldClass(10);
            privateTester = new Core.PrivateTester<PrivateFieldClass>();

            //Assert
            var fieldName = "nonExistingField";
            Assert.Throws<PrivatePropertyException>(
                () => privateTester.GetPrivateField(@object, fieldName)
                );
        }

        [Fact]
        public void Return_10_if_value_of_private_field_is_10()
        {
            //Arrange
            object instance = new PrivateFieldClass(10);
            privateTester = new Core.PrivateTester<PrivateFieldClass>();

            //Act
            var fieldValue = privateTester.GetPrivateField(instance, "privateInt");

            //Assert
            Assert.Equal(10, fieldValue);
        }

        [Fact]
        public void Throws_if_property_is_public()
        {
            //Arrange
            object @object = new PrivateFieldClass(10);
            privateTester = new Core.PrivateTester<PrivateFieldClass>();

            //Assert
            var fieldName = "publicInt";
            Assert.Throws<PrivatePropertyException>(
                () => privateTester.GetPrivateField(@object, fieldName)
                );
        }

        private class PrivateFieldClass
        {
            private int privateInt;
            public int publicInt;

            public PrivateFieldClass(int privateInt)
            {
                this.privateInt = privateInt;
            }
        }
    }
}
