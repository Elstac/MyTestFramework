using Xunit;
using Core.PrivateTester;
namespace Tests.PrivateTester.PrivateMethodFinderTests
{
    public class GetPrivateMethodsTests
    {
        private IPrivateMethodFinder privateMethodsFinder;

        [Fact]
        public void Return_empty_array_if_class_has_no_private_methods()
        {
            //Arrange
            privateMethodsFinder = new PrivateMethodFinder<NoPrivateMembersClass>();

            //Act
            var methodsArray = privateMethodsFinder.GetPrivateMethods();

            //Assert
            Assert.Empty(methodsArray);
        }

        [Fact]
        public void Return_methodinfo_array_if_class_has_private_method()
        {
            //Arrange
            privateMethodsFinder = new PrivateMethodFinder<PrivateMethodsClass>();

            //Act
            var methodsArray = privateMethodsFinder.GetPrivateMethods();

            //Assert
            Assert.Equal(2, methodsArray.Length);
        }

        [Fact]
        public void Return_methodinfo_array_with_protected_methodinfo()
        {
            //Arrange
            privateMethodsFinder = new PrivateMethodFinder<ProtectedMethodClass>();

            //Act
            var methodsArray = privateMethodsFinder.GetPrivateMethods();

            //Assert
            Assert.Single(methodsArray);

            var returnedMethod = methodsArray[0];
            Assert.Equal("ProtectedMethod", returnedMethod.Name);
            Assert.Equal(typeof(void), returnedMethod.ReturnType);
        }

        [Fact]
        public void Array_not_contains_inherited_private_methods()
        {
            //Arrange
            privateMethodsFinder = new PrivateMethodFinder<PrivateMethodSubClass>();

            //Act
            var methodsArray = privateMethodsFinder.GetPrivateMethods();

            //Assert
            Assert.Single(methodsArray);

            var targetMethod = methodsArray[0];
            Assert.Equal("PrivateMethod", targetMethod.Name);
            Assert.Equal(typeof(void), targetMethod.ReturnType);
        }

        class NoPrivateMembersClass
        {

        }

        private class PrivateMethodsClass
        {
            private void PrivateMethod1() { }
            private void PrivateMethod2() { }
        }

        private class PrivateMethodSubClass : PrivateMethodsClass
        {
            private void PrivateMethod() { }
        }

        private class ProtectedMethodClass
        {
            protected void ProtectedMethod() { }
        }
    }
}
