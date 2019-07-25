namespace Core
{

    public interface ITest
    {
        void Run();
        TestReport GetReport();
    }

    public abstract class Test : ITest
    {
        public abstract TestReport GetReport();
        public abstract void Run();
    }
}
