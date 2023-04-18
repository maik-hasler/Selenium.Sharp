using OpenQA.Selenium;

namespace Selenium.Sharp;

public static class WaitConditions
{
    public static Func<ISearchContext, IWebElement> ElementExists(By locator)
    {
        return (searchContext) => searchContext.FindElement(locator);
    }
}