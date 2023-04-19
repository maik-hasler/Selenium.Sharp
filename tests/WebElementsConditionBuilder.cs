using FluentAssertions;
using Moq;
using OpenQA.Selenium;
using Selenium.Sharp;
using System.Collections.ObjectModel;
using Xunit;

namespace SeleniumSharper.Test;

public sealed class WebElementsConditionBuilderTests
{
    private readonly Mock<ISearchContext> _searchContext;

    public WebElementsConditionBuilderTests()
    {
        _searchContext = new Mock<ISearchContext>();
    }

    [Fact]
    public void AreVisible_ShouldReturnTrue_WhenAllElementsAreDisplayed()
    {
        // Arrange
        var fluentWait = new ContextualWait<ISearchContext>(_searchContext.Object, TimeSpan.FromSeconds(2));
        var elements = new List<IWebElement>
        {
            Mock.Of<IWebElement>(e => e.Displayed == true),
            Mock.Of<IWebElement>(e => e.Displayed == true),
            Mock.Of<IWebElement>(e => e.Displayed == true)
        };
        var builder = new WebElementsConditionBuilder<ISearchContext, ReadOnlyCollection<IWebElement>>(fluentWait, _ => new ReadOnlyCollection<IWebElement>(elements));

        // Act
        var result = builder.AreVisible();

        // Assert
        result.AreDisplayed.Should().BeTrue();
    }

    [Fact]
    public void AreVisible_ShouldReturnFalse_WhenAnyElementIsNotDisplayed()
    {
        // Arrange
        var fluentWait = new ContextualWait<ISearchContext>(_searchContext.Object, TimeSpan.FromSeconds(2));
        var elements = new List<IWebElement>
        {
            Mock.Of<IWebElement>(e => e.Displayed == true),
            Mock.Of<IWebElement>(e => e.Displayed == false),
            Mock.Of<IWebElement>(e => e.Displayed == true)
        };
        var builder = new WebElementsConditionBuilder<ISearchContext, ReadOnlyCollection<IWebElement>>(fluentWait, _ => new ReadOnlyCollection<IWebElement>(elements));

        // Act
        var result = builder.AreVisible();

        // Assert
        result.AreDisplayed.Should().BeFalse();
    }

    [Fact]
    public void AreVisible_ShouldReturnTrue_WhenElementBecomesVisible()
    {
        // Arrange
        var fluentWait = new ContextualWait<ISearchContext>(_searchContext.Object, TimeSpan.FromSeconds(5));

        var element1 = new Mock<IWebElement>();
        element1.SetupSequence(e => e.Displayed)
               .Returns(false)
               .Returns(true)
               .Returns(false)
               .Returns(true);

        var element2 = new Mock<IWebElement>();
        element2.SetupSequence(e => e.Displayed)
               .Returns(false)
               .Returns(true);

        var element3 = new Mock<IWebElement>();
        element3.SetupSequence(e => e.Displayed)
               .Returns(true)
               .Returns(false)
               .Returns(true);

        var builder = new WebElementsConditionBuilder<ISearchContext, ReadOnlyCollection<IWebElement>>(
            fluentWait,
            _ => new ReadOnlyCollection<IWebElement>(new[] { element1.Object, element2.Object, element3.Object })
        );

        // Act
        var result = builder.AreVisible();

        // Assert
        result.AreDisplayed.Should().BeTrue();
    }
}