using Core.PrivateTester;
using System;
using Xunit;

namespace Tests.PrivateTester.PrivateMethodFinderTests
{
    public class GetPrivateMethodTests
    {
        private IPrivateMethodFinder privateMethodFinder;

        [Fact]
        public void Throw_if_class_has_no_private_method_with_given_name()
        {
            //Arrange
            privateMethodFinder = new PrivateMethodFinder<NoPrivateMethodClass>();
            var privateMethodName = "PrivateMethod";

            //Assert
            Assert.Throws<InvalidOperationException>(
                () => privateMethodFinder.GetPrivateMethod(privateMethodName)
                );
        }

        [Fact]
        public void Return_methodinfo_if_class_has_private_method_with_given_name()
        {
            //Arrange
            privateMethodFinder = new PrivateMethodFinder<PrivateMethodClass>();
            var privateMethodName = "PrivateMethod";

            //Act
            var privateMethod = privateMethodFinder.GetPrivateMethod(privateMethodName);

            //Assert
            Assert.Equal(privateMethodName, privateMethod.Name);
            Assert.Equal(typeof(void), privateMethod.ReturnType);
            Assert.True(privateMethod.IsPrivate);
        }

        private class NoPrivateMethodClass
        {

        }

        private class PrivateMethodClass
        {
            private void PrivateMethod() { }
        }
    }
}
