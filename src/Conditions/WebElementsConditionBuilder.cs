using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace SeleniumSharper.Conditions;

public sealed class WebElementsConditionBuilder<TSearchContext, TSearchResult>
    where TSearchContext : ISearchContext
    where TSearchResult : IReadOnlyCollection<IWebElement>
{
    private readonly ContextualWait<TSearchContext> _contextualWait;

    private readonly Func<TSearchContext, IReadOnlyCollection<IWebElement>> _action;

    public WebElementsConditionBuilder(ContextualWait<TSearchContext> fluentWait, Func<TSearchContext, IReadOnlyCollection<IWebElement>> action)
    {
        _contextualWait = fluentWait;
        _action = action;
    }

    public IReadOnlyCollection<IWebElement>? AreVisible()
    {
        IReadOnlyCollection<IWebElement>? webElements = null;

        try
        {
            _contextualWait.Wait.Until(ctx =>
            {
                webElements = _action.Invoke(ctx);

                return webElements.All(e => e.Displayed);
            });

            return webElements;
        }
        catch (WebDriverTimeoutException)
        {
            return null;
        }
    }

    public TResult Satisfy<TResult>(Func<IReadOnlyCollection<IWebElement>, TResult> condition)
    {
        return _contextualWait.Wait.Until(ctx =>
        {
            var value = _action.Invoke(ctx);

            return condition(value);
        });
    }
}
