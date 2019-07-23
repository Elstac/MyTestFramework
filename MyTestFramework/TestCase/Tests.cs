using Xunit;

namespace Tests.TestCase
{
    public class Tests
    {
        Core.TestCase testCase;

        [Fact]
        public void Call_methods_in_right_order()
        {
            testCase = new Core.TestCase(() => { }, () => { });

            testCase.Run();

            Assert.Equal("Setup Run Teardown ",testCase.Log);
        }

        [Fact]
        public void Run_test_without_setup_if_setup_is_null()
        {
            //Arrange
            testCase = new Core.TestCase(() => { }, null);

            //Act
            testCase.Run();

            //Assert
            Assert.Contains("Run", testCase.Log);
            Assert.Contains("Teardown", testCase.Log);
        }

        [Fact]
        public void Signal_passed_test_after_test_method_passed()
        {
            testCase = new Core.TestCase(()=> { }, () => { });
            
            testCase.Run();

            Assert.True(testCase.Passed);
        }

        [Fact]
        public void Signal_failed_test_after_test_method_failed()
        {
            testCase = new Core.TestCase(() => throw new System.Exception(), () => { });

            testCase.Run();

            Assert.False(testCase.Passed);
        }

        [Fact]
        public void Signal_failed_test_after_setup_failed()
        {

            testCase = new Core.TestCase(() => { },() => throw new System.Exception());

            testCase.Run();

            Assert.DoesNotContain("Run", testCase.Log);
            Assert.False(testCase.Passed);
        }
    }
}
