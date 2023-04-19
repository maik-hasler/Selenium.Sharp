using OpenQA.Selenium;

namespace SeleniumSharper;

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

    public VisibilityResult IsVisible()
    {
        try
        {
            IWebElement? webElement = null;

            var isDisplayed = _contextualWait.Wait.Until(ctx =>
            {
                webElement = _action.Invoke(ctx);

                return webElement.Displayed;
            });

            return new VisibilityResult
            {
                WebElement = webElement,
                IsVisible = isDisplayed
            };
        }
        catch (WebDriverTimeoutException)
        {
            return new VisibilityResult
            {
                WebElement = null,
                IsVisible = false
            };
        }
    }

    public class VisibilityResult
    {
        public IWebElement? WebElement { get; set; }

        public bool IsVisible { get; set; }
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