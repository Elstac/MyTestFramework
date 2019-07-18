using System;
using System.Linq;
using System.Reflection;

namespace Core
{
    public interface IAssemblyScanner
    {
        Type[] GetTestsFromAssembly(Assembly assembly);
    }

    public class AssemblyScanner : IAssemblyScanner
    {
        private ITestDetector testDetector;

        public AssemblyScanner(ITestDetector testDetector)
        {
            this.testDetector = testDetector;
        }

        public Type[] GetTestsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetTypes();

            var collection = from type in types
                             where testDetector.IsTestFixture(type)
                             select type;

            return collection.ToArray();
        }
    }
}
