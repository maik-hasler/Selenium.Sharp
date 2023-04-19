﻿using OpenQA.Selenium;

namespace SeleniumSharper;

public class ClassConditionBuilder<TSearchContext, TSearchResult>
    where TSearchContext : ISearchContext
    where TSearchResult : IEquatable<string>
{
    private readonly ContextualWait<TSearchContext> _contextualWait;

    private readonly Func<TSearchContext, string> _action;

    public ClassConditionBuilder(ContextualWait<TSearchContext> fluentWait, Func<TSearchContext, string> action)
    {
        _contextualWait = fluentWait;
        _action = action;
    }

    public TResult Satisfies<TResult>(Func<string, TResult> condition)
    {
        return _contextualWait.Wait.Until(ctx =>
        {
            var value = _action.Invoke(ctx);

            return condition(value);
        });
    }
}