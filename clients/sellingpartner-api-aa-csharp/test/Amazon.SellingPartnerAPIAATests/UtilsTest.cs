using System;
using System.Linq;
using System.Text;
using Xunit;
using Amazon.SellingPartnerAPIAA;

namespace Amazon.SellingPartnerAPIAATests
{
    public class UtilsTest
    {
        private const string TestString = "test";

        [Fact]
        public void TestUrlEncode_WithoutEncoding()
        {
            string result = Utils.UrlEncode("Test-_.~");
            Assert.Equal("Test-_.~", result);
        }

        [Fact]
        public void TestUrlEncode_WithEncoding()
        {
            string result = Utils.UrlEncode("Test$%*^");
            Assert.Equal("Test%24%25%2A%5E", result);
        }

        [Fact]
        public void TestUrlEncode_Empty()
        {
            Assert.Empty(Utils.UrlEncode(string.Empty));
        }

        [Fact]
        public void TestHash()
        {
            Assert.NotEmpty(Utils.Hash(TestString));
        }

        [Fact]
        public void TestToHex()
        {
            string result = Utils.ToHex(Encoding.UTF8.GetBytes(TestString));
            Assert.Equal("74657374", result);
        }

        [Fact]
        public void TestGetKeyedHash()
        {
            byte[] expectedHash = new byte[] { 106, 120, 238, 51, 86, 30, 87, 173, 232, 197, 95, 132,155,
                183, 80, 81, 25, 213, 212, 241, 218, 201, 168, 17, 253, 143, 54, 226, 42, 118, 61, 54 };
            byte[] keyedHash = Utils.GetKeyedHash(Encoding.UTF8.GetBytes("testKey"), TestString);
            Assert.True(expectedHash.SequenceEqual(keyedHash));
        }
    }
}
