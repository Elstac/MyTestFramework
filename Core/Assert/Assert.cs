using System;
using System.Collections.Generic;

namespace Core
{
    public class MyAssert
    {
        public static void Equal<T>(T expected,T actual)
        {
            if (!expected.Equals(actual))
                throw new AssertException($"Actual value differ from expected.\nExpected: {expected}\nActual: {actual}");
        }

        public static void Contains(string actual, string substring)
        {
            if(string.IsNullOrEmpty(substring))
                throw new AssertException($"Given substring is null or empty.\n");

            if (!actual.Contains(substring))
                throw new AssertException($"Given string doesnt contain given substring\nString: {actual}\nSubstring: {substring}");    
        }

        public static void Contains<T>(IEnumerable<T> array, T element)
        {
            foreach (var item in array)
                if (item.Equals(element))
                    return;

            throw new AssertException();
        }

        public static void Fail(string message)
        {
            throw new AssertException(message);
        }
        

        public static void Throws<T>(Action action)where T:Exception
        {
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(T))
                    return;

                throw new AssertException($"Exception of invalid type thrown.\n Exception type: {e.GetType()}\nExpected type: {typeof(T)}");
            }

            throw new AssertException($"No exception thrown.\nExpeced type {typeof(T)}");
        }

        public static void ThrowsNothing(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception)
            {
                throw new AssertException("Exception thrown.");
            }
        }

        public static void ThrowsAny(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception)
            {
                return;
            }

            throw new AssertException("No exception thrown.");
        }
    }
}
