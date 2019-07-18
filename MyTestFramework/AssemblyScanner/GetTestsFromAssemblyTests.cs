using Core;
using System;
using System.Reflection;
using System.Reflection.Emit;
using Xunit;

namespace Tests.AssemblyScanner
{
    public class GetTestsFromAssemblyTests
    {
        private IAssemblyScanner assemblyScanner;
        private ModuleBuilder moduleBuilder;

        public GetTestsFromAssemblyTests()
        {
            var aName = new AssemblyName("TestAssembly");
            var builder =
                AssemblyBuilder.DefineDynamicAssembly(
                    aName,
                    AssemblyBuilderAccess.Run);

            moduleBuilder = builder.DefineDynamicModule("TestModule");
        }
        //Scan
        [Fact]
        public void Return_empty_array_if_no_test_fixtures_types_in_assembly()
        {
            //Arrange
            assemblyScanner = new Core.AssemblyScanner(new TestDetector());

            //Act
            Type[] result = assemblyScanner.GetTestsFromAssembly(moduleBuilder.Assembly);

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Returned_array_contains_test_fixture_type()
        {
            //Arrange
            assemblyScanner = new Core.AssemblyScanner(new TestDetector());
            var testFixtureType = CreateAssemblyWithSingleTestFixtureWithSingleTest();

            //Act
            var result = assemblyScanner.GetTestsFromAssembly(moduleBuilder.Assembly);

            //Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Contains(testFixtureType, result);
        }

        private Type CreateAssemblyWithSingleTestFixtureWithSingleTest()
        {
            var typeBuilder = moduleBuilder.DefineType(
                            "TestFixture",
                            TypeAttributes.Public | TypeAttributes.Class);

            var methodBuilder = typeBuilder.DefineMethod(
                "TestMethod",
                MethodAttributes.Public,
                typeof(void),
                new Type[] { });

            var ILGen = methodBuilder.GetILGenerator();
            ILGen.Emit(OpCodes.Ret);

            var CABuilder = new CustomAttributeBuilder(
                typeof(Core.TestAttribute).GetConstructor(new Type[] { }),
                new object[] { }
                );

            methodBuilder.SetCustomAttribute(CABuilder);

            return typeBuilder.CreateType();
        }
    }
}
