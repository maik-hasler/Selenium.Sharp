using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace SeleniumSharper;

public sealed class WebElementsConditionBuilder<TSearchContext, TSearchResult>
    where TSearchContext : ISearchContext
    where TSearchResult : ReadOnlyCollection<IWebElement>
{
    private readonly ContextualWait<TSearchContext> _contextualWait;

    private readonly Func<TSearchContext, ReadOnlyCollection<IWebElement>> _action;

    public WebElementsConditionBuilder(ContextualWait<TSearchContext> fluentWait, Func<TSearchContext, ReadOnlyCollection<IWebElement>> action)
    {
        _contextualWait = fluentWait;
        _action = action;
    }

    public bool AreVisible()
    {
        try
        {
            return _contextualWait.Wait.Until(ctx =>
            {
                var elements = _action.Invoke(ctx);

                return elements.All(e => e.Displayed);
            });
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }

    public TResult Satisfy<TResult>(Func<ReadOnlyCollection<IWebElement>, TResult> condition)
    {
        return _contextualWait.Wait.Until(ctx =>
        {
            var value = _action.Invoke(ctx);

            return condition(value);
        });
    }
}