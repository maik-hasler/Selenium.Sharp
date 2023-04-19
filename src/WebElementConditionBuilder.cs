using OpenQA.Selenium;

namespace Selenium.Sharp;

public class WebElementConditionBuilder<TSearchContext, TSearchResult> 
    where TSearchContext : ISearchContext 
    where TSearchResult : IWebElement
{
    private readonly ContextualWait<TSearchContext> _fluentWait;

    private readonly Func<TSearchContext, IWebElement> _action;

    public WebElementConditionBuilder(ContextualWait<TSearchContext> fluentWait, Func<TSearchContext, IWebElement> action)
    {
        _fluentWait = fluentWait;
        _action = action;
    }

    public bool IsVisible()
    {
        try
        {
            return _fluentWait.Wait.Until(ctx =>
            {
                var element = _action.Invoke(ctx);

                return element.Displayed;
            });
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }
}