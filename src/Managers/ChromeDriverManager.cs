using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumSharper.Managers.Interfaces;
using SeleniumSharper.Managers.Services;
using System.Runtime.InteropServices;

namespace SeleniumSharper.Managers;

public sealed class ChromeDriverManager : IWebDriverManager<ChromeOptions>
{
    public IWebDriver Setup()
    {
        var driverPath = InstallBinary();

        var chromeDriverService = ChromeDriverService.CreateDefaultService(driverPath);

        return new ChromeDriver(chromeDriverService);
    }

    public IWebDriver Setup(ChromeOptions chromeOptions)
    {
        var driverPath = InstallBinary();

        var chromeDriverService = ChromeDriverService.CreateDefaultService(driverPath);

        return new ChromeDriver(chromeDriverService, chromeOptions);
    }

    private static string GetBinaryName()
    {
        var suffix = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? ".exe" : string.Empty;

        return $"chromedriver{suffix}";
    }

    private static Uri GetDownloadUrl(string version, string fileName)
    {
        var url = $"https://chromedriver.storage.googleapis.com/{version}/{fileName}";

        return new Uri(url);
    }

    private static string GetFileName()
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

    private static string GetLatestVersion()
    {
        var url = "https://chromedriver.storage.googleapis.com/LATEST_RELEASE";

        using var httpClient = new HttpClient();

        var response = httpClient.GetStringAsync(url).Result;

        return response.Trim();
    }

    private static string GetBinaryPath(string version)
    {
        var architecture = Environment.Is64BitOperatingSystem ? "64" : "32";

        return Path.Combine(
            Directory.GetCurrentDirectory(),
            "Binaries",
            "Chrome",
            version,
            architecture,
            GetBinaryName());
    }

    private static string InstallBinary()
    {
        var version = GetLatestVersion();

        var fileName = GetFileName();

        var downloadUrl = GetDownloadUrl(version, fileName);

        var binaryPath = GetBinaryPath(version);

        return WebDriverManagerUtils.InstallBinary(fileName, downloadUrl, binaryPath, GetBinaryName());
    }
}
