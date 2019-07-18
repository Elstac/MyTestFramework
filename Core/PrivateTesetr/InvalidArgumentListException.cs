using System;

namespace Core
{
    public class InvalidArgumentListException : Exception
    {
        public InvalidArgumentListException(string message) : base(message)
        {
        }
        public InvalidArgumentListException()
        {

        }
    }
}
