using Xunit;
using Core;
using System.Collections.Generic;

namespace Tests.AssertTests
{
    public class ContainsTests
    {
        [Fact]
        public void Throws_if_string_does_not_contain_substring()
        {
            //Assert
            Assert.Throws<Core.AssertException>(() => Core.MyAssert.Contains("xx", "xd"));
        }

        [Fact]
        public void Throws_nothing_if_string_contains_substring()
        {
            try
            {
                Core.MyAssert.Contains("pppdfp", "df");
            }
            catch (System.Exception)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Throws_if_substring_is_empty()
        {
            //Assert
            Assert.Throws<Core.AssertException>(() => Core.MyAssert.Contains("xx", ""));
        }

        [Fact]
        public void Throws_if_substring_is_null()
        {
            //Assert
            Assert.Throws<Core.AssertException>(() => Core.MyAssert.Contains("xx", null));
        }

        [Fact]
        public void Throws_if_integer_array_does_not_contain_given_element()
        {
            //Arrange
            var array = new int[] { 1, 2, 4 };

            //Assert
            Assert.Throws<AssertException>(() => MyAssert.Contains(array, 10));
        }

        [Fact]
        public void Throws_nothing_if_array_contains_element()
        {
            //Arrange
            var array = new int[] { 1, 2, 4 };

            //Assert
            try
            {
                Core.MyAssert.Contains(array, 4);
            }
            catch (System.Exception)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Throws_if_string_array_does_not_contain_given_element()
        {
            //Arrange
            var array = new string[] { "a", "b", "d" };

            //Assert
            Assert.Throws<AssertException>(() => MyAssert.Contains(array, "c"));
        }

        [Fact]
        public void Throws_nothing_if_string_array_contains_element()
        {
            //Arrange
            var array = new string[] { "a", "b", "d" };

            //Assert
            try
            {
                Core.MyAssert.Contains(array, "d");
            }
            catch (System.Exception)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Throws_if_string_list_does_not_contain_given_element()
        {
            //Arrange
            var list = new List<string> { "a", "b", "d" };

            //Assert
            Assert.Throws<AssertException>(() => MyAssert.Contains(list, "c"));
        }

        [Fact]
        public void Throws_nothing_if_string_list_contains_element()
        {
            //Arrange
            var list = new List<string> { "a", "b", "d" };

            //Assert
            try
            {
                Core.MyAssert.Contains(list, "d");
            }
            catch (System.Exception)
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Throws_if_int_list_does_not_contain_given_element()
        {
            //Arrange
            var list = new List<int> { 1, 2, 4 };

            //Assert
            Assert.Throws<AssertException>(() => MyAssert.Contains(list, 3));
        }

        [Fact]
        public void Throws_nothing_if_integer_list_contains_element()
        {
            //Arrange
            var list = new List<int> { 1, 2, 4 };

            //Assert
            try
            {
                Core.MyAssert.Contains(list, 1);
            }
            catch (System.Exception)
            {
                Assert.True(false);
            }
        }
    }
}
