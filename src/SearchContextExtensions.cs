using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace Selenium.Sharp;

/// <summary>
/// The SearchContextExtensions class provides extension methods for the ISearchContext interface.
/// </summary>
public static class SearchContextExtensions
{
    /// <summary>
    /// Returns a Waiter object that can be used to wait for a specified condition to be satisfied before continuing with program execution.
    /// </summary>
    /// <param name="searchContext">The search context to be used for the wait operation.</param>
    /// <param name="timeoutInSeconds">The maximum amount of time to wait for the condition to be satisfied, in seconds. If set to 0 (default), the wait will continue indefinitely.</param>
    /// <returns>A Waiter object that can be used to wait for a specified condition to be satisfied before continuing with program execution.</returns>
    public static Waiter<ISearchContext> Wait(this ISearchContext searchContext, int timeoutInSeconds = 0)
    {
        var wait = new DefaultWait<ISearchContext>(searchContext)
        {
            Timeout = TimeSpan.FromSeconds(timeoutInSeconds)
        };

        return new Waiter<ISearchContext>(wait);
    }
}