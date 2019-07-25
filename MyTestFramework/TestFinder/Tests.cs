using Core;
using System.Reflection;
using Xunit;
namespace Tests.TestFinder
{
    public class Tests
    {
        private ITestDetector detector;

        [Fact]
        public void Return_null_if_class_has_no_teardown_method()
        {
            //Arrange
            detector = new TestDetector();

            //Act
            MethodInfo result = detector.GetTeardownMethodInfo(typeof(SimpleClass));

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Return_teardown_method_if_class_has_teardown_method()
        {
            //Arrange
            detector = new TestDetector();
            var setup = typeof(TestFixture).GetMethod("TearDown");

            //Act
            MethodInfo result = detector.GetTeardownMethodInfo(typeof(TestFixture));

            //Assert
            Assert.Equal(setup, result);
        }

        [Fact]
        public void Return_null_if_class_has_no_setup_method()
        {
            //Arrange
            detector = new TestDetector();

            //Act
            MethodInfo result = detector.GetSetupMethodInfo(typeof(SimpleClass));

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Return_setup_method_if_class_has_setup_method()
        {
            //Arrange
            detector = new TestDetector();
            var setup = typeof(TestFixture).GetMethod("Setup");

            //Act
            MethodInfo result = detector.GetSetupMethodInfo(typeof(TestFixture));

            //Assert
            Assert.Equal(setup,result);
        }

        [Fact]
        public void Return_empty_array_if_class_is_not_test_fixture()
        {
            //Arrange
            detector = new TestDetector();

            //Act
            MethodInfo[] detectedTests = detector.GetTestMethods(typeof(SimpleClass));

            //Assert
            Assert.Empty(detectedTests);
        }

        [Fact]
        public void Return_collection_of_methods_if_class_contains_at_least_one()
        {
            //Arrange
            detector = new TestDetector();

            //Act
            MethodInfo[] detectedTests = detector.GetTestMethods(typeof(TestFixture));

            //Assert
            var expected = typeof(TestFixture).GetMethod("TestMethod");

            Assert.Single(detectedTests);
            Assert.Equal(expected,detectedTests[0]);
        }
    }

    class TestFixture
    {
        [Setup]
        public void Setup()
        {

        }

        [Test]
        public void TestMethod()
        {

        }

        [Teardown]
        public void TearDown()
        {

        }

    }
    class MultipleTestFixture:TestFixture
    {
        [Test]
        public void SecondTestMethod()
        {

        }
    }

    class SimpleClass
    {
        public void SimpleMethod()
        {

        }
    }
}
