using OpenQA.Selenium;

namespace Selenium.Sharp.Extensions;

internal static class JavaScriptExecutorExtensions
{
    public static void Click(this IJavaScriptExecutor executor, IWebElement elementToBeClicked)
    {
        executor.ExecuteScript("arguments[0].click();", elementToBeClicked);
    }
}