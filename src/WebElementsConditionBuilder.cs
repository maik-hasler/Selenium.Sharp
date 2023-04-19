using OpenQA.Selenium;
using SeleniumSharper.Models;
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

    public WebElementsVisibilityResult AreVisible()
    {
        try
        {
            ReadOnlyCollection<IWebElement>? webElements = null;

            var areDisplayed = _contextualWait.Wait.Until(ctx =>
            {
                webElements = _action.Invoke(ctx);

                return webElements.All(e => e.Displayed);
            });

            return new WebElementsVisibilityResult
            {
                WebElements = webElements,
                AreDisplayed = areDisplayed
            };
        }
        catch (WebDriverTimeoutException)
        {
            return new WebElementsVisibilityResult
            {
                WebElements = null,
                AreDisplayed = false
            };
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