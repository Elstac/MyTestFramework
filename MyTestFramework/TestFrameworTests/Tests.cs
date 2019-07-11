using Moq;
using System;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using Xunit;

namespace Tests.TestFrameworTests
{
    public class Tests:IDisposable
    {
        private Core.TestFramework testFramework;
        private ModuleBuilder moduleBuilder;

        public Tests()
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
            testFramework = new Core.TestFramework(new Core.TestDetector(), null, null);

            //Act
            Type[] result = testFramework.ScanAssembly(moduleBuilder.Assembly);

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Returned_array_contains_test_fixture_type()
        {
            //Arrange
            testFramework = new Core.TestFramework(new Core.TestDetector(), null, null);
            var testFixtureType = CreateAssemblyWithSingleTestFixtureWithSingleTest();

            //Act
            var result = testFramework.ScanAssembly(moduleBuilder.Assembly);

            //Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Contains(testFixtureType, result);
        }

        //[Fact]
        //public void TEST()
        //{
        //    CreateAssemblyWithSingleTestFixtureWithSingleTest();
        //    var mock = new Mock<Core.IDirectoryScanner>();
        //    mock.Setup(m => m.ScanDirectory(It.IsAny<string>()))
        //        .Returns(new Assembly[] { moduleBuilder.Assembly });

        //    testFramework = new Core.TestFramework(
        //        new Core.TestDetector(),
        //        new Core.TestFixtureFactory(new Core.TestDetector()),
        //        mock.Object
        //        );

        //    testFramework.Run("");
        //}

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

        public void Dispose()
        {
            var testTmpDirectory = "/testTmp";

            if (Directory.Exists(testTmpDirectory))
                Directory.Delete(testTmpDirectory);
        }
    }
}
