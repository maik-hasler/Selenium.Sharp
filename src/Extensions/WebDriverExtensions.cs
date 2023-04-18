using OpenQA.Selenium;

namespace Selenium.Sharp.Extensions;

internal static class WebDriverExtensions
{
    public static IJavaScriptExecutor Scripts(this IWebDriver webDriver)
    {
        return (IJavaScriptExecutor)webDriver;
    }
}