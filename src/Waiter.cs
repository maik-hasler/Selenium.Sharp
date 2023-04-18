using OpenQA.Selenium.Support.UI;

namespace Selenium.Sharp;

/// <summary>
/// The Waiter class provides a convenient way to wait for a specified condition to be satisfied.
/// </summary>
/// <typeparam name="T">The type of object on which the wait it to be applied.</typeparam>
public sealed class Waiter<T>
{
    private readonly DefaultWait<T> _wait;

    /// <summary>
    /// Initializes a new instance of the Waiter class.
    /// </summary>
    /// <param name="wait">The DefaultWait object that is used to wait for the specified condition to be satisfied.</param>
    public Waiter(DefaultWait<T> wait)
    {
        _wait = wait;
    }

    /// <summary>
    /// Waits until the specified condition is satisfied or until the timeout is reached.
    /// </summary>
    /// <typeparam name="TResult">The type of result returned by the specified condition.</typeparam>
    /// <param name="condition">The condition to be checked.</param>
    /// <returns>The result of the specified condition.</returns>
    public TResult Until<TResult>(Func<T, TResult> condition)
    {
        return _wait.Until(condition);
    }
}