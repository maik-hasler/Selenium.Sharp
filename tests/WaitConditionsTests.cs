using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Moq;
using OpenQA.Selenium;
using Xunit;

namespace Selenium.Sharp.Test;

public class WaitConditionsTests
{
    private readonly Mock<ISearchContext> _searchContextMock;

    public WaitConditionsTests()
    {
        _searchContextMock = new Mock<ISearchContext>();
    }

    [Fact]
    public void ElementExists_ReturnsWebElement_WhenElementIsFound()
    {
        // Arrange
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        var expectedElement = fixture.Create<Mock<IWebElement>>().Object;
        var locator = By.Id("test");
        _searchContextMock.Setup(x => x.FindElement(locator)).Returns(expectedElement);

        // Act
        var result = WaitConditions.ElementExists(locator).Invoke(_searchContextMock.Object);

        // Assert
        result.Should().Be(expectedElement);
    }

    [Fact]
    public void ElementExists_ThrowsNoSuchElementException_WhenElementIsNotFound()
    {
        // Arrange
        var locator = By.Id("test");
        _searchContextMock.Setup(x => x.FindElement(locator)).Throws(new NoSuchElementException());

        // Act
        Action act = () => WaitConditions.ElementExists(locator).Invoke(_searchContextMock.Object);

        // Assert
        act.Should().Throw<NoSuchElementException>();
    }

    [Fact]
    public void ElementIsVisible_ReturnsTrue_WhenElementIsDisplayed()
    {
        // Arrange
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        var expectedElement = fixture.Create<Mock<IWebElement>>();
        expectedElement.Setup(x => x.Displayed).Returns(true);
        var locator = By.Id("test");
        _searchContextMock.Setup(x => x.FindElement(locator)).Returns(expectedElement.Object);

        // Act
        var result = WaitConditions.ElementIsVisible(locator).Invoke(_searchContextMock.Object);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void ElementIsVisible_ReturnsFalse_WhenElementIsNotDisplayed()
    {
        // Arrange
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        var expectedElement = fixture.Create<Mock<IWebElement>>();
        expectedElement.Setup(x => x.Displayed).Returns(false);
        var locator = By.Id("test");
        _searchContextMock.Setup(x => x.FindElement(locator)).Returns(expectedElement.Object);

        // Act
        var result = WaitConditions.ElementIsVisible(locator).Invoke(_searchContextMock.Object);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void ElementIsVisible_ThrowsNoSuchElementException_WhenElementIsNotFound()
    {
        // Arrange
        var locator = By.Id("test");
        _searchContextMock.Setup(x => x.FindElement(locator)).Throws(new NoSuchElementException());

        // Act
        Action act = () => WaitConditions.ElementIsVisible(locator).Invoke(_searchContextMock.Object);

        // Assert
        act.Should().Throw<NoSuchElementException>();
    }
}