using Xunit;
using Moq;
using Core;
using System;

namespace Tests.TestFixture
{
    public class TestFixtureTests
    {
        private Core.TestFixture testFixture;

        [Fact]
        public void Start_test_fixture_with_empty_list()
        {
            testFixture = new Core.TestFixture();

            var tesetFromCollection = testFixture.Get(0);

            Assert.Null(tesetFromCollection);
        }

        [Fact]
        public void Fixture_contains_newly_added_test()
        {
            testFixture = new Core.TestFixture();
            var newTest = new Core.TestCase(null, null);

            testFixture.Add(newTest);
            var tesetFromCollection = testFixture.Get(0);

            Assert.Equal(newTest, tesetFromCollection);
        }

        [Fact]
        public void Run_all_tests()
        {
            //Arrange
            testFixture = new Core.TestFixture();
            var tests = new Mock<ITest>[]
            {
                new Mock<ITest>(),
                new Mock<ITest>()
            };
            foreach (var test in tests)
                testFixture.Add(test.Object);

            //Act
            testFixture.Run();

            //Assert
            foreach (var test in tests)
                test.Verify(t => t.Run(), Times.Once());
        }

        [Fact]
        public void Whole_fixture_fails_after_at_least_one_test_fails()
        {
            //Arrange
            testFixture = new Core.TestFixture();
            var failedTest = new Core.TestCase(() => { }, () => throw new Exception());
            var succsessfulTest = new Core.TestCase(() => { }, () => { });

            testFixture.Add(succsessfulTest);
            testFixture.Add(failedTest);

            //Act
            testFixture.Run();

            //Assert
            Assert.False(testFixture.Passed);
        }

        [Fact]
        public void Signal_succsess_when_no_test_run()
        {
            //Arrange
            testFixture = new Core.TestFixture();

            //Act
            testFixture.Run();

            //Assert
            Assert.True(testFixture.Passed);
        }
    }
}
