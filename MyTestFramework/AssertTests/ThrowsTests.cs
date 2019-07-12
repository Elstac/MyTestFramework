using Xunit;
using Core;
using System;

namespace Tests.AssertTests
{
    public class ThrowsTests
    {
        [Fact]
        public void Throws_if_nothing_thrown()
        {
            //Arrange
            Action action = () => { };

            //Assert
            Assert.Throws<Core.AssertException>(
                () => Core.MyAssert.Throws<Exception>(action)
                );
        }

        [Fact]
        public void Throws_if_exception_of_other_type_thrown()
        {
            //Arrange
            Action action = () => { throw new InvalidCastException(); };

            //Assert
            Assert.Throws<Core.AssertException>(
                () => Core.MyAssert.Throws<DuplicateWaitObjectException>(action)
                );
        }

        [Fact]
        public void Pass_if_exception_of_given_type_thrown()
        {
            //Arrange
            Action action = () => { throw new DuplicateWaitObjectException(); };

            try
            {
                Core.MyAssert.Throws<DuplicateWaitObjectException>(action);
            }
            catch (System.Exception)
            {
                Assert.True(false);
            }
        }
    }

}
