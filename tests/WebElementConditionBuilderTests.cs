using FluentAssertions;
using Moq;
using OpenQA.Selenium;
using Selenium.Sharp;
using Xunit;

namespace SeleniumSharper.Test;

public sealed class WebElementConditionBuilderTests
{
    private readonly Mock<IWebDriver> _webDriver;

    public WebElementConditionBuilderTests()
    {
        _webDriver = new Mock<IWebDriver>();
    }

    [Fact]
    public void IsVisible_ShouldReturnTrue_WhenElementIsDisplayed()
    {
        // Arrange
        var fluentWait = new ContextualWait<IWebDriver>(_webDriver.Object, TimeSpan.FromSeconds(2));
        var element = new Mock<IWebElement>();
        element.SetupGet(e => e.Displayed).Returns(true);
        var builder = new WebElementConditionBuilder<IWebDriver, IWebElement>(fluentWait, _ => element.Object);

        // Act
        var result = builder.IsVisible();

        // Assert
        result.IsVisible.Should().BeTrue();
    }

    [Fact]
    public void IsVisible_ShouldReturnFalse_WhenElementIsNotDisplayed()
    {
        // Arrange
        var fluentWait = new ContextualWait<IWebDriver>(_webDriver.Object, TimeSpan.FromSeconds(2));
        var element = new Mock<IWebElement>();
        element.SetupGet(e => e.Displayed).Returns(false);
        var builder = new WebElementConditionBuilder<IWebDriver, IWebElement>(fluentWait, _ => element.Object);

        // Act
        var result = builder.IsVisible();

        // Assert
        result.IsVisible.Should().BeFalse();
    }

    [Fact]
    public void IsVisible_ShouldReturnTrue_WhenElementBecomesDisplayed()
    {
        // Arrange
        var fluentWait = new ContextualWait<IWebDriver>(_webDriver.Object, TimeSpan.FromSeconds(2));
        var element = new Mock<IWebElement>();
        element.SetupSequence(e => e.Displayed)
               .Returns(false)
               .Returns(true);
        var builder = new WebElementConditionBuilder<IWebDriver, IWebElement>(fluentWait, _ => element.Object);

        // Act
        var result = builder.IsVisible();

        // Assert
        result.IsVisible.Should().BeTrue();
        element.VerifyGet(e => e.Displayed, Times.Exactly(2));
    }
}