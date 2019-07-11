using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Core
{
    public class TestFramework
    {
        private ITestDetector testDetector;
        private ITestFixtureFactory testFixtureFactory;
        private IDirectoryScanner directoryScanner;

        public TestFramework(
            ITestDetector testDetector,
            ITestFixtureFactory testFixtureFactory, 
            IDirectoryScanner directoryScanner
            )
        {
            this.testDetector = testDetector;
            this.testFixtureFactory = testFixtureFactory;
            this.directoryScanner = directoryScanner;
        }

        public Type[] ScanAssembly(Assembly assembly)
        {
            var types = assembly.GetTypes();

            var collection = from type in types
                             where testDetector.IsTestFixture(type)
                             select type;

            return collection.ToArray();
        }
        
        public string Run(string directory)
        {
            var ret = new List<ITest>();

            var assemblies = directoryScanner.ScanDirectory(directory);

            foreach (var assembly in assemblies)
            {
                var testClasses = ScanAssembly(assembly);

                foreach (var testClass in testClasses)
                    ret.Add(testFixtureFactory.GetTestFixture(testClass));
            }

            var report = new StringBuilder();

            foreach (var testFixture in ret)
            {
                testFixture.Run();
                report.Append(testFixture.GetReport());
            }

            return report.ToString();
        }
    }
}
