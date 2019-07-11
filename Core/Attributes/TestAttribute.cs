using System;

namespace Core
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TestAttribute:Attribute
    {
        public TestAttribute()
        {

        }
    }
}
