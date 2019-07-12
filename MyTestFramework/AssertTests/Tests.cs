﻿using Xunit;

namespace Tests.AssertTests
{
    public class Tests
    {
        //Equals
        [Fact]
        public void Throws_if_integers_are_not_equal()
        {
            //Assert
            Assert.Throws<Core.AssertException>(() => Core.MyAssert.Equal(1, 2));
        }

        [Fact]
        public void Throws_nothing_if_integers_are_equal()
        {
            try
            {
                Core.MyAssert.Equal(1, 1);
            }
            catch (System.Exception)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Throws_if_strings_are_diffrent()
        {
            //Assert
            Assert.Throws<Core.AssertException>(() => Core.MyAssert.Equal("xx", "xd"));
        }

        [Fact]
        public void Throws_if_strings_are_equal()
        {
            try
            {
                Core.MyAssert.Equal("xd", "xd");
            }
            catch (System.Exception)
            {
                Assert.True(false);
            }
        }
    }
}
