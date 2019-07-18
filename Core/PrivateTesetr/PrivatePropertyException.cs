using System;
using System.Collections.Generic;
using System.Text;

namespace Core.PrivateTester
{
    public class PrivatePropertyException : Exception
    {
        public PrivatePropertyException()
        {

        }

        public PrivatePropertyException(string message) : base(message)
        {
        }
    }
}
