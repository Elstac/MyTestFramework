using System;
using System.Linq;
using System.Reflection;

namespace Core
{
    public interface ITestDetector
    {
        MethodInfo GetSetupMethodInfo(Type type);
        MethodInfo[] GetTestMethods(Type type);
        bool IsTestFixture(Type type);
    }

    public class TestDetector:ITestDetector
    {
        private bool HasAttribute<T>(MethodInfo method) where T : Attribute
        {
            var setUpAttribute = method.GetCustomAttribute<T>();
            return setUpAttribute != null;
        }

        public bool IsTestFixture(Type type)
        {
            return GetTestMethods(type).Length > 0;
        }
        
        public MethodInfo GetSetupMethodInfo(Type type)
        {
            var methods = type.GetMethods();

            return (from method in methods
                                where HasAttribute<SetupAttribute>(method)
                                select method)
                                .FirstOrDefault();
            
        }

        public MethodInfo[] GetTestMethods(Type type)
        {
            var methods = type.GetMethods();

            return (from method in methods
                    where HasAttribute<TestAttribute>(method)
                    select method)
                    .ToArray();
        }
        
    }
}
