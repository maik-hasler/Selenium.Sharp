using Moq;
using OpenQA.Selenium;
using Selenium.Sharp;
using Xunit;

namespace SeleniumSharper.Test;

public sealed class SearchContextExtensionsTests
{
    [Fact]
    public void Wait_ReturnsNewContextualWaitInstance()
    {
        // Arrange
        var searchContext = new Mock<ISearchContext>().Object;

        // Act
        var result = searchContext.Wait();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ContextualWait<ISearchContext>>(result);
    }

    [Fact]
    public void Wait_ReturnsNewContextualWaitInstance_WithSpecifiedTimeout()
    {
        // Arrange
        var searchContext = new Mock<ISearchContext>().Object;
        var timeout = 10;

        // Act
        var result = searchContext.Wait(timeout);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ContextualWait<ISearchContext>>(result);
        Assert.Equal(TimeSpan.FromSeconds(timeout), result.Wait.Timeout);
    }
}