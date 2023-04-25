using OpenQA.Selenium;
using SeleniumSharper.Conditions;

namespace SeleniumSharper.Extensions;

public static class SearchContextExtensions
{
    public static ContextualWait<TSearchContext> Wait<TSearchContext>(this TSearchContext searchContext, int timeoutInSeconds = 0)
        where TSearchContext : ISearchContext
    {
        return new ContextualWait<TSearchContext>(searchContext, TimeSpan.FromSeconds(timeoutInSeconds));
    }
}
