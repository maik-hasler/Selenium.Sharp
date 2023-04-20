using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Runtime.InteropServices;

namespace SeleniumSharper.Managers;

public sealed class ChromeDriverManager : WebDriverManagerBase, IWebDriverManager<ChromeOptions>
{
    public IWebDriver Setup()
    {
        var driverPath = InstallBinary();

        var chromeDriverService = ChromeDriverService.CreateDefaultService(driverPath);

        return new ChromeDriver(chromeDriverService);
    }

    protected override string GetBinaryName()
    {
        var suffix = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? ".exe" : string.Empty;

        return $"chromedriver{suffix}";
    }

    protected override Uri GetDownloadUrl(string version, string fileName)
    {
        var url = $"https://chromedriver.storage.googleapis.com/{version}/{fileName}";

        return new Uri(url);
    }

    protected override string GetFileName()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            var architectureExtension = RuntimeInformation.ProcessArchitecture == Architecture.Arm64 ? "_arm64" : "64";

            return $"chromedriver_mac{architectureExtension}.zip";
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return "chromedriver_linux64.zip";
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return "chromedriver_win32.zip";
        }

        throw new PlatformNotSupportedException();
    }

    protected override string GetLatestVersion()
    {
        var url = "https://chromedriver.storage.googleapis.com/LATEST_RELEASE";

        using var httpClient = new HttpClient();

        var response = httpClient.GetStringAsync(url).Result;

        return response.Trim();
    }
}