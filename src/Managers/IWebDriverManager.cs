using OpenQA.Selenium;
using SeleniumSharper.Managers.Enums;

namespace SeleniumSharper.Managers;

public interface IWebDriverManager<T>
    where T : IWebDriver
{
    public string Setup(VersionResolveStrategy versionResolveStrategy, string? version);
}
