using OpenQA.Selenium.Support.UI;

namespace Selenium.Sharp;

public sealed class Waiter<T>
{
    private readonly DefaultWait<T> _wait;

    public Waiter(DefaultWait<T> wait)
    {
        _wait = wait;
    }

    public TResult Until<TResult>(Func<T, TResult> condition)
    {
        return _wait.Until(condition);
    }
}