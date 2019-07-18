using System;
using Xunit;

namespace Tests.PrivateTester.MainPrivateTesterTests
{
    public class GetPrivateTypeTests
    {
        private Core.IPrivateTester privateTester;

        [Fact]
        public void Return_string_type_if_string_type_given()
        {
            //Arrange
            privateTester = new Core.PrivateTester<string>();

            //Act
            var type = privateTester.GetPrivateType();

            //Asssert
            Assert.Equal(typeof(string), type);
        }
    }
}
