using OpenQA.Selenium;

namespace SeleniumSharper;

public static class WebDriverExtensions
{
    public static IJavaScriptExecutor Scripts(this IWebDriver webDriver)
    {
        return (IJavaScriptExecutor)webDriver;
    }
}