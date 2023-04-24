using OpenQA.Selenium;
using SeleniumSharper.Managers.Enums;

namespace SeleniumSharper.Managers.Interfaces;

public interface IWebDriverManager<T>
    where T : IWebDriver
{
    public string Setup();

    public WebDriverManager<T> WithVersion(VersionResolveStrategy versionResolveStrategy);

    public WebDriverManager<T> WithVersion(string version);
}
