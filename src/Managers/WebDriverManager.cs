using OpenQA.Selenium;
using SeleniumSharper.Managers.Enums;

namespace SeleniumSharper.Managers;

public static class WebDriverManager
{
    public static string Setup<T>(VersionResolveStrategy versionResolveStrategy, string? version)
        where T : IWebDriver
    {
        var webDriverManager = FindWebDriverManager<T>();

        return webDriverManager.Setup(versionResolveStrategy, version);
    }

    private static IWebDriverManager<T> FindWebDriverManager<T>()
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
