using OpenQA.Selenium;

namespace SeleniumSharper;

public static class JavaScriptExecutorExtensions
{
    public static void Click(this IJavaScriptExecutor executor, IWebElement elementToBeClicked)
    {
        executor.ExecuteScript("arguments[0].click();", elementToBeClicked);
    }

    public static void DispatchEvent(this IJavaScriptExecutor executor, IWebElement webElement, DomEvent domEvent)
    {
        var domEventName = domEvent.ToString().ToLower();

        executor.ExecuteScript("[0].dispatchEvent(new Event('[1]'));", webElement, domEventName);
    }
}