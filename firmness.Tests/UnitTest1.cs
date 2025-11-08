using Xunit;
namespace firmness.Tests
{
    public class BasicTests
    {
        [Fact]
        public void Project_Should_Run_Tests_Correctly()
        {
            int a = 2;
            int b = 3;
            int sum = a + b;

            Assert.Equal(5, sum);
        }
    }
}