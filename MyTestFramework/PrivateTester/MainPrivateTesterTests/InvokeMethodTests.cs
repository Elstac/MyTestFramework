using Core.PrivateTester;
using System;
using Xunit;

namespace Tests.PrivateTester.MainPrivateTesterTests
{
    public class InvokeMethodTests:IDisposable
    {
        private Core.IPrivateTester privateTester;


        [Fact]
        public void Throw_if_class_has_no_method_with_given_type()
        {
            //Arrange
            privateTester = new Core.PrivateTester<PrivateMethodClass>(
                new PrivateMethodFinder<PrivateMethodClass>()
                );

            //Assert
            var methodName = "NotExistentMethod";
            Assert.Throws<InvalidOperationException>(
                () => privateTester.InvokePrivateMethod(methodName)
                );
        }

        [Fact]
        public void Call_method_with_given_name()
        {
            //Arrange
            privateTester = new Core.PrivateTester<PrivateMethodClass>(
                new PrivateMethodFinder<PrivateMethodClass>()
                );

            //Act
            privateTester.InvokePrivateMethod("PrivateMethod");

            //Assert
            Assert.Equal(1, PrivateMethodClass.privateMethodCalls);
            Assert.Equal(0, PrivateMethodClass.publicMethodCalls);
        }

        [Fact]
        public void Call_method_with_given_name_and_provide_parameters()
        {
            //Arrange
            privateTester = new Core.PrivateTester<PrivateMethodClass>(
                new PrivateMethodFinder<PrivateMethodClass>()
                );

            //Act
            privateTester.InvokePrivateMethod("PrivateMethodWithParameter", 1);

            //Assert
            Assert.Equal(1, PrivateMethodClass.privateMethodCalls);
            Assert.Equal(0, PrivateMethodClass.publicMethodCalls);
            Assert.Equal(1, PrivateMethodClass.passedParameter);
        }

        [Fact]
        public void Throw_if_method_require_no_parameter_but_parameter_provided()
        {
            //Arrange
            privateTester = new Core.PrivateTester<PrivateMethodClass>(
                new PrivateMethodFinder<PrivateMethodClass>()
                );

            //Assert
            var methodName = "PrivateMethod";
            Assert.Throws<Core.InvalidArgumentListException>(
                () => privateTester.InvokePrivateMethod(methodName, 1)
                );
        }

        [Fact]
        public void Throw_if_parameter_number_is_diffrent_than_required()
        {
            //Arrange
            privateTester = new Core.PrivateTester<PrivateMethodClass>(
                 new PrivateMethodFinder<PrivateMethodClass>()
                 );

            //Assert
            var methodName = "PrivateMethodWithParameter";
            Assert.Throws<Core.InvalidArgumentListException>(
                () => privateTester.InvokePrivateMethod(methodName, 1, 3)
                );
        }

        [Fact]
        public void Throw_if_method_require_parameter_but_no_parameter_provided()
        {
            //Arrange
            privateTester = new Core.PrivateTester<PrivateMethodClass>(
                new PrivateMethodFinder<PrivateMethodClass>()
                );

            //Assert
            var methodName = "PrivateMethodWithParameter";
            Assert.Throws<Core.InvalidArgumentListException>(
                () => privateTester.InvokePrivateMethod(methodName)
                );
        }
        
        public void Dispose()
        {
            PrivateMethodClass.privateMethodCalls = 0;
            PrivateMethodClass.publicMethodCalls = 0;
            PrivateMethodClass.passedParameter = 0;
        }

        private class PrivateMethodClass
        {
            public static int privateMethodCalls = 0;
            public static int publicMethodCalls = 0;
            public static int passedParameter = 0;

            private void PrivateMethod() { privateMethodCalls++; }
            private void PrivateMethodWithParameter(int param)
            {
                privateMethodCalls++;
                passedParameter = param;
            }

            private void PrivateMethodWithTwoParameters(int param, int param2)
            {
                privateMethodCalls++;
                passedParameter = param;
            }

            public void PublicMethod() { publicMethodCalls++; }
        }
    }
}
