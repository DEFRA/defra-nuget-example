using Defra.Example.Greetings;

namespace Defra.Example.Greetings.Tests;

public class GreetingTests
{
    [Fact]
    public void Hello_ReturnsGreeting()
    {
        var result = Greeting.Hello("John");

        Assert.Equal("Hello, John!", result);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Hello_RejectsEmptyName(string name)
    {
        Assert.Throws<ArgumentException>(() => Greeting.Hello(name));
    }
}
