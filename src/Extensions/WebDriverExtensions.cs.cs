using OpenQA.Selenium;

namespace SeleniumSharper.Extensions;

public static class WebDriverExtensions
{
    public static IJavaScriptExecutor Scripts(this IWebDriver webDriver)
    {
        return (IJavaScriptExecutor)webDriver;
    }
}
