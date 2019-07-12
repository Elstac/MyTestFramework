using System;
using System.Collections.Generic;

namespace Core
{
    public class MyAssert
    {
        public static void Equal<T>(T expected,T actual)
        {
            if (!expected.Equals(actual))
                throw new AssertException();
        }

        public static void Contains(string actual, string substring)
        {
            if(string.IsNullOrEmpty(substring) || !actual.Contains(substring))
                throw new AssertException();    
        }

        public static void Contains<T>(IEnumerable<T> array, T element)
        {
            foreach (var item in array)
                if (item.Equals(element))
                    return;

            throw new AssertException();
        }

        public static void Fail()
        {
            throw new AssertException();
        }
    }
}
