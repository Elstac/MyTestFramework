using Core;
using System;
using Tests.TestFinder;
using Xunit;

namespace Tests.TestFixtureFactory
{
    public class GetTestFixtureTests : IDisposable
    {
        private ITestFixtureFactory testDetector;

        [Fact]
        public void Throw_if_class_has_no_test_methods()
        {
            //Arrange
            testDetector = new Core.TestFixtureFactory(new TestDetector());

            //Assert
            Assert.Throws<InvalidOperationException>(
                () => testDetector.GetTestFixture(typeof(SimpleClass)));
        }

        [Fact]
        public void Create_test_case_if_class_has_only_one_test()
        {
            //Arrange
            testDetector = new Core.TestFixtureFactory(new TestDetector());

            //Act
            var createdTest = testDetector.GetTestFixture(typeof(MockFixture));

            //Assert
            Assert.IsType<Core.TestCase>(createdTest);
        }

        [Fact]
        public void Create_test_with_setup_when_class_has_setup()
        {
            //Arrange
            testDetector = new Core.TestFixtureFactory(new TestDetector());

            //Act
            var createdTest = testDetector.GetTestFixture(typeof(MockFixture));
            createdTest.Run();

            //Assert
            Assert.Equal(1, MockFixture.setupRun);
        }

        [Fact]
        public void Create_test_fixture_containing_all_tests()
        {
            //Arrange
            testDetector = new Core.TestFixtureFactory(new TestDetector());

            //Act
            var createdTest = testDetector.GetTestFixture(typeof(MockFixture2));
            createdTest.Run();

            //Assert
            Assert.Equal(2, MockFixture.testRun);
        }

        [Fact]
        public void Create_test_fixture_with_add_test_cases_containing_setup()
        {
            //Arrange
            testDetector = new Core.TestFixtureFactory(new TestDetector());

            //Act
            var createdTest = testDetector.GetTestFixture(typeof(MockFixture2));
            createdTest.Run();

            //Assert
            Assert.Equal(2, MockFixture.setupRun);
        }

        [Fact]
        public void Create_test_fixture_if_class_has_multiple_tests()
        {
            //Arrange
            testDetector = new Core.TestFixtureFactory(new TestDetector());

            //Act
            var createdTest = testDetector.GetTestFixture(typeof(MultipleTestFixture));

            //Assert
            Assert.IsType<Core.TestFixture>(createdTest);
        }

        [Fact]
        public void Create_test_without_setup_if_no_setup_method_present()
        {
            //Arrange
            testDetector = new Core.TestFixtureFactory(new TestDetector());

            //Act
            testDetector.GetTestFixture(typeof(NoSetupMock)).Run();

            //Assert
            Assert.Equal(1, NoSetupMock.testRun);
        }

        public void Dispose()
        {
            MockFixture.setupRun = 0;
            MockFixture.testRun = 0;
            NoSetupMock.testRun = 0;
        }

        class MockFixture
        {
            public static int setupRun = 0;
            public static int testRun = 0;

            [Setup]
            public void Setup() => setupRun++;
            [Test]
            public void Test() => testRun++;
        }

        class MockFixture2 : MockFixture
        {
            [Test]
            public void Test2() => testRun++;
        }

        class NoSetupMock
        {
            public static int testRun = 0;
            [Test]
            public void Test() => testRun++;
        }
    }
}
