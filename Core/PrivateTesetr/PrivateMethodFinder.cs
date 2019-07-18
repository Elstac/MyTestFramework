using System;
using System.Linq;
using System.Reflection;

namespace Core.PrivateTester
{
    public interface IPrivateMethodFinder
    {
        MethodInfo[] GetPrivateMethods();
        MethodInfo GetPrivateMethod(string privateMethodName);
    }

    public class PrivateMethodFinder<T>:IPrivateMethodFinder
    {
        public MethodInfo[] GetPrivateMethods()
        {
            var allMethods = typeof(T).GetMethods(
                BindingFlags.NonPublic |
                BindingFlags.Instance
                );

            //Exclude costructor and methods inherited from System.Object
            return (from method in allMethods
                    where !method.IsConstructor && method.DeclaringType != typeof(object)
                    select method).ToArray();
        }

        public MethodInfo GetPrivateMethod(string privateMethodName)
        {
            var privateMethods = GetPrivateMethods();

            var targetMethod = (from method in privateMethods
                                where method.Name.Equals(privateMethodName)
                                select method).FirstOrDefault();

            if (targetMethod == null)
                throw new InvalidOperationException();

            return targetMethod;
        }
    }
}
