namespace Core
{
    public class MyAssert
    {
        public static void Equal<T>(T expected,T actual)
        {
            if (!expected.Equals(actual))
                throw new AssertException();
        }
    }
}
