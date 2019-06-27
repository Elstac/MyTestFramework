using Core;
using Xunit;
namespace Tests.TestCase
{
    public class Tests
    {
        private Core.TestCase testCase;

        [Fact]
        public void Call_test_method_from_test_class()
        {
            testCase = new Core.TestCase(typeof(TestClass));

            testCase.Run("Test");

            Assert.True(testCase.Runned);
        }
    }
}
