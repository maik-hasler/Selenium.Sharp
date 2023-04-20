using OpenQA.Selenium;

namespace SeleniumSharper.Conditions;

public class WebElementConditionBuilder<TSearchContext, TSearchResult>
    where TSearchContext : ISearchContext
    where TSearchResult : IWebElement
{
    private readonly ContextualWait<TSearchContext> _contextualWait;

    private readonly Func<TSearchContext, IWebElement> _action;

    public WebElementConditionBuilder(ContextualWait<TSearchContext> fluentWait, Func<TSearchContext, IWebElement> action)
    {
        _contextualWait = fluentWait;
        _action = action;
    }

    public IWebElement? IsVisible()
    {
        IWebElement? webElement = null;

        try
        {
            _contextualWait.Wait.Until(ctx =>
            {
                webElement = _action.Invoke(ctx);

                return webElement.Displayed;
            });

            return webElement;
        }
        catch (WebDriverTimeoutException)
        {
            return null;
        }
    }

    public TResult Satisfies<TResult>(Func<IWebElement, TResult> condition)
    {
        return _contextualWait.Wait.Until(ctx =>
        {
            var value = _action.Invoke(ctx);

            return condition(value);
        });
    }
}