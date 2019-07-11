using System;
using System.Reflection;

namespace Core
{
    public interface ITestFixtureFactory
    {
        ITest GetTestFixture(Type testFixtureType);
    }

    public class TestFixtureFactory:ITestFixtureFactory
    {
        private ITestDetector testDetector;

        public TestFixtureFactory(ITestDetector testDetector)
        {
            this.testDetector = testDetector;
        }
        
        public ITest GetTestFixture(Type testFixtureType)
        {
            return CreateTests(
                testDetector.GetTestMethods(testFixtureType),
                testDetector.GetSetupMethodInfo(testFixtureType)
                );
        }

        public ITest CreateTests(MethodInfo[] testMethodsInfo, MethodInfo setupInfo)
        {
            if (testMethodsInfo.Length > 1)
            {
                var fixture = new TestFixture();

                var setup = GetAction(setupInfo);

                foreach (var testInfo in testMethodsInfo)
                {
                    var test = GetAction(testInfo);
                    fixture.Add(new TestCase(test, setup));
                }

                return fixture;
            }

            if (testMethodsInfo.Length > 0)
            {
                var setup = GetAction(setupInfo);

                var test = GetAction(testMethodsInfo[0]);

                return new TestCase(test, setup);
            }

            throw new InvalidOperationException();
        }

        private Action GetAction(MethodInfo methodInfo)
        {
            if (methodInfo == null)
                return null;

            var instance = Activator.CreateInstance(methodInfo.DeclaringType);

            var action = methodInfo.CreateDelegate(
                typeof(Action),
                instance) as Action;

            return action;
        }
    }
}
