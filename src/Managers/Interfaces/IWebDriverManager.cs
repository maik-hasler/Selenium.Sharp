using OpenQA.Selenium;

namespace SeleniumSharper.Managers.Interfaces;

public interface IWebDriverManager<T>
    where T : IWebDriver
{
    public string Setup();

    public WebDriverManagerBase<T> With(Action<WebDriverManagerConfiguration> configure);
}
