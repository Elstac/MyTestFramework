using System;

namespace Core
{
    public class AssertException:Exception
    {
        public AssertException()
        {

        }

        public AssertException(string msg) : base(msg)
        {

        }
    }
}