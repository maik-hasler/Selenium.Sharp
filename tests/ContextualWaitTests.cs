using FluentAssertions;
using Moq;
using OpenQA.Selenium;
using SeleniumSharper.Conditions;
using Xunit;

namespace SeleniumSharper.Test;

public sealed class ContextualWaitTests
{
    private readonly Mock<IWebDriver> _searchContext;

    private readonly TimeSpan _timeout;

    public ContextualWaitTests()
    {
        _searchContext = new Mock<IWebDriver>();
        _timeout = TimeSpan.FromSeconds(30);
    }

    [Fact]
    public void Until_ReturnsWebElementConditionBuilder()
    {
        // Arrange
        var wait = new ContextualWait<IWebDriver>(_searchContext.Object, _timeout);

        // Act
        var result = wait.Until(ctx => ctx.FindElement(By.Id("example")));

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<WebElementConditionBuilder<IWebDriver, IWebElement>>();
    }

    [Fact]
    public void Until_ReturnsClassConditionBuilder()
    {
        // Arrange
        var wait = new ContextualWait<IWebDriver>(_searchContext.Object, _timeout);

        // Act
        var result = wait.Until(ctx => ctx.Title);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<StringConditionBuilder<IWebDriver, string>>();
    }
}
