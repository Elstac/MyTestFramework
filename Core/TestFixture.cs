using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class TestFixture:Test
    {
        List<ITest> tests;
        private int passed;
        private int failed;
        private string name;

        public TestFixture()
        {
            name = "TestFixture";
            Passed = true;
            tests = new List<ITest>();
            passed = 0;
            failed = 0;
        }

        public TestFixture(string name)
        {
            this.name = name;
            Passed = true;
            tests = new List<ITest>();
            passed = 0;
            failed = 0;
        }

        public ITest Get(int index)
        {
            return tests.Count>index?tests[index]:null;
        }

        public void Add(ITest newTest)
        {
            tests.Add(newTest);
        }
        
        public override void Run()
        {
            foreach (var test in tests)
            {
                test.Run();
                if (!test.Passed)
                {
                    Passed = false;
                    failed++;
                }
                else
                    passed++;
            }
        }

        public override string GetReport()
        {
            var sb = new StringBuilder();

            sb.Append($"{name}:\n");
            sb.Append($"Test passed: {passed}. Test failed: {failed}.\n");

            foreach (var test in tests)
                sb.Append(test.GetReport()+"\n");

            return sb.ToString();
        }
    }
}
