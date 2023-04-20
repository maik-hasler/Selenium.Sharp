using Moq;
using OpenQA.Selenium;
using Xunit;
using FluentAssertions;
using SeleniumSharper.Conditions;

namespace SeleniumSharper.Test;

public sealed class StringConditionBuilderTests
{
    private readonly Mock<IWebDriver> _webDriver;

    public StringConditionBuilderTests()
    {
        _webDriver = new Mock<IWebDriver>();
    }

    [Fact]
    public void Satisfies_ShouldReturnTrue_WhenConditionIsSatisfied()
    {
        // Arrange
        var fluentWait = new ContextualWait<IWebDriver>(_webDriver.Object, TimeSpan.FromSeconds(2));
        var expectedValue = "Example Value";
        var action = new Func<IWebDriver, string>(driver =>
        {
            return expectedValue;
        });
        var builder = new StringConditionBuilder<IWebDriver, string>(fluentWait, action);

        // Act
        var result = builder.Satisfies(value => value.Equals(expectedValue));

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Satisfies_ShouldThrowTimeoutException_WhenConditionIsNotSatisfied()
    {
        // Arrange
        var fluentWait = new ContextualWait<IWebDriver>(_webDriver.Object, TimeSpan.FromSeconds(2));
        var expectedValue = "Example Value";
        var action = new Func<IWebDriver, string>(driver =>
        {
            return "Another Value";
        });
        var builder = new StringConditionBuilder<IWebDriver, string>(fluentWait, action);

        // Act & Assert
        Assert.Throws<WebDriverTimeoutException>(() =>
        {
            builder.Satisfies(value => value.Equals(expectedValue));
        });
    }

    [Fact]
    public void Satisfies_ShouldReturnTrue_WhenConditionBecomesSatisfied()
    {
        // Arrange
        var expectedTitle = "Example Page";
        var titleAccessCount = 0;
        _webDriver.Setup(d => d.Title).Returns(() =>
        {
            titleAccessCount++;
            return titleAccessCount == 1 ? "Wrong Title" : expectedTitle;
        });

        var builder = new StringConditionBuilder<IWebDriver, string>(
            new ContextualWait<IWebDriver>(_webDriver.Object, TimeSpan.FromSeconds(30)),
            context => context.Title
        );

        // Act
        var result = builder.Satisfies(title => title.Equals(expectedTitle));

        // Assert
        result.Should().BeTrue();
        _webDriver.VerifyGet(d => d.Title, Times.Exactly(2));
    }
}