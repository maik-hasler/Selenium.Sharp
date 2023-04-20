using OpenQA.Selenium;

namespace SeleniumSharper.Managers;

public interface IWebDriverManager<TOptions>
    where TOptions : DriverOptions
{
    public IWebDriver Setup();
}