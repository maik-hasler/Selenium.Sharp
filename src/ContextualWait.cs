using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumSharper;

public sealed class ContextualWait<TSearchContext>
    where TSearchContext : ISearchContext
{
    private readonly TSearchContext _searchContext;

    private readonly IWait<TSearchContext> _wait;

    public ContextualWait(TSearchContext searchContext, TimeSpan timeout)
    {
        _searchContext = searchContext;

        _wait = new DefaultWait<TSearchContext>(_searchContext)
        {
            Timeout = timeout
        };
    }

    public IWait<TSearchContext> Wait { get { return _wait; } }

    public WebElementConditionBuilder<TSearchContext, IWebElement> Until(Func<TSearchContext, IWebElement> action)
    {
        return new WebElementConditionBuilder<TSearchContext, IWebElement>(this, action);
    }

    public ClassConditionBuilder<TSearchContext, string> Until(Func<TSearchContext, string> action)
    {
        return new ClassConditionBuilder<TSearchContext, string>(this, action);
    }
}