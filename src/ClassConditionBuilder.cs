using OpenQA.Selenium;

namespace Selenium.Sharp;

public class ClassConditionBuilder<TSearchContext, TSearchResult>
    where TSearchContext : ISearchContext
    where TSearchResult : class
{
    private readonly ContextualWait<TSearchContext> _fluentWait;

    private readonly Func<TSearchContext, string> _action;

    public ClassConditionBuilder(ContextualWait<TSearchContext> fluentWait, Func<TSearchContext, string> action)
    {
        _fluentWait = fluentWait;
        _action = action;
    }

    public TResult Satisfies<TResult>(Func<string, TResult> condition)
    {
        return _fluentWait.Wait.Until(ctx =>
        {
            var value = _action.Invoke(ctx);

            return condition(value);
        });
    }
}