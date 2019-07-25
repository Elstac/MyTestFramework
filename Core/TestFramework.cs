using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class TestFramework
    {
        private ITestFixtureFactory testFixtureFactory;
        private IDirectoryScanner directoryScanner;
        private IAssemblyScanner assemblyScanner;

        public TestFramework(
            ITestFixtureFactory testFixtureFactory, 
            IDirectoryScanner directoryScanner,
            IAssemblyScanner assemblyScanner
            )
        {
            this.testFixtureFactory = testFixtureFactory;
            this.directoryScanner = directoryScanner;
            this.assemblyScanner = assemblyScanner;
        }
        
        public IEnumerable<TestReport> Run(string directory)
        {
            var ret = new List<ITest>();

            var assemblies = directoryScanner.ScanDirectory(directory);

            foreach (var assembly in assemblies)
            {
                var testClasses = assemblyScanner.GetTestsFromAssembly(assembly);

                foreach (var testClass in testClasses)
                    ret.Add(testFixtureFactory.GetTestFixture(testClass));
            }

            var report = new List<TestReport>();

            foreach (var fixture in ret)
            {
                fixture.Run();
                report.Add(fixture.GetReport());
            }

            return report;
        }
    }
}
