using MauiMemoryleak;

namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(4, 2+2);
        }

        [Fact]
        public void Test2()
        {
            var returnValue = ReturnString.ReturnValue;
            Assert.Equal("This Is NOT Android AND NOT Platform", returnValue);
        }
    }
}
