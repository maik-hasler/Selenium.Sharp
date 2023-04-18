using OpenQA.Selenium;

namespace Selenium.Sharp;

/// <summary>
/// Provides a collection of commonly used selenium wait conditions.
/// </summary>
public static class WaitConditions
{
    /// <summary>
    /// Returns a function that checks if an element with the specified locator exists on the page.
    /// </summary>
    /// <param name="by">The locator of the element to search for.</param>
    /// <returns>A function that takes an ISearchContext and returns an IWebElement.</returns>
    public static Func<ISearchContext, IWebElement> ElementExists(By by)
    {
        return (searchContext) => searchContext.FindElement(by);
    }

    /// <summary>
    /// Returns a function that checks if an element with the specified locator is visible on the page.
    /// </summary>
    /// <param name="by">The locator of the element to search for.</param>
    /// <returns>A function that takes an ISearchContext and returns a bool indicating if the element is visible.</returns>
    public static Func<ISearchContext, bool> ElementIsVisible(By by)
    {
        return (searchContext) =>
        {
            var element = searchContext.FindElement(by);

            return element.Displayed;
        };
    }
}