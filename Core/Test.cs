namespace Core
{

    public interface ITest
    {
        void Run();
        bool Passed { get; set; }
        string GetReport();
    }

    public abstract class Test : ITest
    {
        public bool Passed { get; set; }
        public abstract string GetReport();
        public abstract void Run();
    }
}
