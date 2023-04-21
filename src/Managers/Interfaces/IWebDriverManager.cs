using OpenQA.Selenium;

namespace SeleniumSharper.Managers.Interfaces;

public interface IWebDriverManager<TOptions>
    where TOptions : DriverOptions
{
    public IWebDriver Setup();

    public IWebDriver Setup(TOptions options);
}
