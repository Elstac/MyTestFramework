using System;
using System.Reflection;

namespace Core
{
    public class AssertException:Exception
    {
        public MethodInfo AssertMethodInfo { get; set; }
        public AssertException()
        {

        }

        public AssertException(string msg) : base(msg)
        {

        }

        public AssertException(MethodInfo assertMethodInfo)
        {
            AssertMethodInfo = assertMethodInfo;
        }
    }
}