using Moq;
using System;
using Xunit;

namespace Tests.TestCase
{
    public class Tests : IDisposable
    {
        Core.TestCase testCase;

        [Fact]
        public void Call_methods_in_right_order()
        {
            testCase = new Core.TestCase(() => { }, () => { },()=> { });

            testCase.Run();

            Assert.Equal("Setup Run Teardown ",testCase.Log);
        }

        [Fact]
        public void Run_test_without_setup_if_setup_is_null()
        {
            //Arrange
            testCase = new Core.TestCase(() => { }, null, ()=>{ });

            //Act
            testCase.Run();

            //Assert
            Assert.Contains("Run", testCase.Log);
            Assert.Contains("Teardown", testCase.Log);
        }

        [Fact]
        public void Run_teardown_method_if_not_null()
        {
            //Arrange
            testCase = new Core.TestCase(() => { }, () => { }, TearDownMock.TearDown);

            //Act
            testCase.Run();

            //Assert
            Assert.Equal(1, TearDownMock.run);
        }

        public void Dispose()
        {
            TearDownMock.run = 0;
        }

        private class TearDownMock
        {
            public static int run = 0;

            public static void TearDown()
            {
                run++;
            }
        }
    }
}
