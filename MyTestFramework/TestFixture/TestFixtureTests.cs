using Core;
using Moq;
using Xunit;

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
            var newTest = new Core.TestCase(() => { }, () => { });

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
            {
                test.Setup(t => t.GetReport()).Returns(new TestReport());

                testFixture.Add(test.Object);
            }

            //Act
            testFixture.Run();

            //Assert
            foreach (var test in tests)
                test.Verify(t => t.Run(), Times.Once());
        }
    }
}
