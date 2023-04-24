using OpenQA.Selenium.Firefox;
using SeleniumSharper.Managers.Enums;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace SeleniumSharper.Managers;

public sealed class FirefoxDriverManager : WebDriverManager<FirefoxDriver>
{
    private static string GetArchiveName(string version)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            var architectureExtension =
                RuntimeInformation.ProcessArchitecture == Architecture.Arm64
                    ? "-aarch64"
                    : string.Empty;

            return $"geckodriver-v{version}-macos-{architectureExtension}.tar.gz";
        }

        var architecture = Environment.Is64BitOperatingSystem ? "64" : "32";

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return $"geckodriver-v{version}-linux-{architecture}.tar.gz";
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return $"geckodriver-v{version}-win-{architecture}.tar.gz";
        }

        throw new PlatformNotSupportedException();
    }

    protected override string GetBinaryName()
    {
        var suffix = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? ".exe" : string.Empty;

        return $"geckodriver{suffix}";
    }

    protected override string GetDownloadUrl(string version)
    {
        var archiveName = GetArchiveName(version);

        return $"https://github.com/mozilla/geckodriver/releases/download/v{version}/{archiveName}";
    }

    protected override string GetName()
    {
        return "Firefox";
    }

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

    private static string GetLatestVersion()
    {
        using var httpClient = new HttpClient();

        var response = httpClient.GetStringAsync("https://api.github.com/repos/mozilla/geckodriver/releases/latest").Result;

        var content = JsonDocument.Parse(response);

        var version = content.RootElement
            .GetProperty("name")
            .GetString()
            ?? throw new NullReferenceException("Unable to retrieve latest version from GitHub API.");

        return version;
    }

    private string GetInstalledVersion()
    {
        throw new NotImplementedException();
    }
}
