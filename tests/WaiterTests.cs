using AutoFixture;
using Moq;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using Xunit;
using FluentAssertions;
using System.Reflection;
using AutoFixture.AutoMoq;

namespace Selenium.Sharp.Test;

public class WaiterTests
{
    [Fact]
    public void Wait_ReturnsWaiterInstance()
    {
        // Arrange
        var searchContext = new Mock<ISearchContext>().Object;

        // Act
        var waiter = searchContext.Wait();

        // Assert
        waiter.Should().NotBeNull();
        waiter.Should().BeOfType<Waiter<ISearchContext>>();
    }

    [Fact]
    public void Wait_UsesSpecifiedTimeout()
    {
        // Arrange
        var timeoutInSeconds = 10;
        var searchContext = new Mock<ISearchContext>().Object;

        // Act
        var waiter = searchContext.Wait(timeoutInSeconds);
        var waitField = typeof(Waiter<ISearchContext>).GetField("_wait", BindingFlags.NonPublic | BindingFlags.Instance);
        var waitValue = waitField!.GetValue(waiter) as DefaultWait<ISearchContext>;

        // Assert
        waiter.Should().NotBeNull();
        waiter.Should().BeOfType<Waiter<ISearchContext>>();
        waitValue!.Timeout.Should().Be(TimeSpan.FromSeconds(timeoutInSeconds));
    }

    [Fact]
    public void Wait_Until_ReturnsExpectedResult()
    {
        // Arrange
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        var expectedElement = fixture.Create<Mock<IWebElement>>().Object;
        var locator = By.Id("test");
        var searchContextMock = new Mock<ISearchContext>();
        searchContextMock.Setup(x => x.FindElement(locator)).Returns(expectedElement);
        var waiter = new Waiter<ISearchContext>(new DefaultWait<ISearchContext>(searchContextMock.Object));

        // Act
        var result = waiter.Until(WaitConditions.ElementExists(locator));

        // Assert
        result.Should().Be(expectedElement);
    }

    [Fact]
    public void Wait_Until_ThrowsNoSuchElementException_WhenElementIsNotFound()
    {
        // Arrange
        var locator = By.Id("test");
        var searchContextMock = new Mock<ISearchContext>();
        searchContextMock.Setup(x => x.FindElement(locator)).Throws(new NoSuchElementException());
        var waiter = new Waiter<ISearchContext>(new DefaultWait<ISearchContext>(searchContextMock.Object));

        // Act
        Action act = () => waiter.Until(WaitConditions.ElementExists(locator));

        // Assert
        act.Should().Throw<NoSuchElementException>();
    }
}