using OpenQA.Selenium;
using SeleniumSharper.Managers.Interfaces;

namespace SeleniumSharper.Managers;

/// <summary>
/// Provides a simple way to create an instance of an <see cref="IWebDriverManager{T}"/> for a specific <see cref="IWebDriver"/> type.
/// </summary>
public static class WebDriverManager
{
    /// <summary>
    /// Creates an instance of an <see cref="IWebDriverManager{T}"/> for the specified <see cref="IWebDriver"/> type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The <see cref="IWebDriver"/> type to create an instance of.</typeparam>
    /// <returns>An instance of an <see cref="IWebDriverManager{T}"/>.</returns>
    /// <exception cref="NotSupportedException">Thrown when the specified <see cref="IWebDriver"/> type is not supported.</exception>
    /// <exception cref="NullReferenceException">Thrown when the instance of the <see cref="IWebDriverManager{T}"/> could not be created.</exception>
    public static IWebDriverManager<T> For<T>()
        where T : IWebDriver
    {
        var desiredManagerType = typeof(IWebDriverManager<>).MakeGenericType(typeof(T));

        var managerType = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .FirstOrDefault(x => x.IsClass && !x.IsAbstract && desiredManagerType.IsAssignableFrom(x))
            ?? throw new NotSupportedException($"WebDriver type '{typeof(T)}' is not supported.");

        var managerInstance = Activator.CreateInstance(managerType)
            ?? throw new NullReferenceException($"Failed to create instance of '{managerType}'.");

        return (IWebDriverManager<T>)managerInstance;
    }
}
