using OpenQA.Selenium.Chrome;
using SeleniumSharper.Managers.Enums;
using System.Runtime.InteropServices;

namespace SeleniumSharper.Managers;

public sealed class ChromeDriverManager : WebDriverManagerBase<ChromeDriver>
{
    protected override string GetVersion(VersionResolveStrategy versionResolveStrategy, string? version)
    {
        return versionResolveStrategy switch
        {
            VersionResolveStrategy.LatestVersion => GetLatestVersion(),
            VersionResolveStrategy.InstalledVersion => GetInstalledVersion(),
            VersionResolveStrategy.SpecificVersion when !string.IsNullOrWhiteSpace(version) => version!,
            VersionResolveStrategy.SpecificVersion => throw new ArgumentException("A specific version is required but none was provided.", nameof(version)),
            _ => throw new ArgumentException($"Unknown version strategy: {versionResolveStrategy}", nameof(versionResolveStrategy))
        };
    }

    private static string GetArchiveName()
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

    protected override string GetBinaryName()
    {
        var suffix = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? ".exe" : string.Empty;

        return $"chromedriver{suffix}";
    }

    protected override string GetName()
    {
        return "Chrome";
    }

    private static string GetLatestVersion()
    {
        var url = "https://chromedriver.storage.googleapis.com/LATEST_RELEASE";

        using var httpClient = new HttpClient();

        var response = httpClient.GetStringAsync(url).Result;

        return response.Trim();
    }

    private string GetInstalledVersion()
    {
        throw new NotImplementedException();
    }

    protected override string GetDownloadUrl(string version)
    {
        var archiveName = GetArchiveName();

        return $"https://chromedriver.storage.googleapis.com/{version}/{archiveName}";
    }
}
