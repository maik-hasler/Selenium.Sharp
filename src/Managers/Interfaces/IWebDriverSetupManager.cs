using OpenQA.Selenium;
using SeleniumSharper.Managers.Enums;

namespace SeleniumSharper.Managers.Interfaces;

public interface IWebDriverSetupManager<TDriverOptions, TDriverService>
    where TDriverOptions : DriverOptions
    where TDriverService : DriverService
{
    public IWebDriver Setup();

    public IWebDriver Setup(TDriverOptions driverOptions);

    public IWebDriver Setup(VersionResolveStrategy versionResolveStrategy);

    public IWebDriver Setup(TDriverService driverService);

    public IWebDriver Setup(string driverVersion);

    public IWebDriver Setup(VersionResolveStrategy versionResolveStrategy, TDriverOptions driverOptions);

    public IWebDriver Setup(TDriverService driverService, TDriverOptions driverOptions);

    public IWebDriver Setup(string driverVersion, TDriverOptions driverOptions);
}
