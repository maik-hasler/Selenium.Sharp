using OpenQA.Selenium;

namespace SeleniumSharper;

public sealed class WebElementVisibilityResult
{
    public IWebElement? WebElement { get; set; }

    public bool IsVisible { get; set; }
}