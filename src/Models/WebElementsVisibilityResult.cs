using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace SeleniumSharper.Models;

public sealed class WebElementsVisibilityResult
{
    public ReadOnlyCollection<IWebElement>? WebElements { get; set; }

    public bool AreDisplayed { get; set; }
}