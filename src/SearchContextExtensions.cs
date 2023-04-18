using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace Selenium.Sharp;

public static class SearchContextExtensions
{
    public static Waiter<ISearchContext> Wait(this ISearchContext searchContext, int timeoutInSeconds = 0)
    {
        var wait = new DefaultWait<ISearchContext>(searchContext)
        {
            Timeout = TimeSpan.FromSeconds(timeoutInSeconds)
        };

        return new Waiter<ISearchContext>(wait);
    }
}