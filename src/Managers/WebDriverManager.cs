using OpenQA.Selenium;
using SeleniumSharper.Managers.Interfaces;

namespace SeleniumSharper.Managers;

public static class WebDriverManager
{
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
