using OpenQA.Selenium;

namespace Selenium.Sharp;

public static class WaitConditions
{
    public static Func<ISearchContext, IWebElement> ElementExists(By by)
    {
        return (searchContext) => searchContext.FindElement(by);
    }

    public static Func<ISearchContext, bool> ElementIsVisible(By by)
    {
        return (searchContext) =>
        {
            var element = searchContext.FindElement(by);

            return element.Displayed;
        };
    }
}