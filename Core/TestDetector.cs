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
        MethodInfo GetTeardownMethodInfo(Type type);
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
            return GetMethodWithAttribute<SetupAttribute>(type);
        }

        public MethodInfo GetTeardownMethodInfo(Type type)
        {
            return GetMethodWithAttribute<TeardownAttribute>(type);
        }

        private MethodInfo GetMethodWithAttribute<T>(Type type) where T:Attribute
        {
            var methods = type.GetMethods();

            return (from method in methods
                    where HasAttribute<T>(method)
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
