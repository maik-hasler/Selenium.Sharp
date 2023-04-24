using OpenQA.Selenium;
using SeleniumSharper.Managers.Common;
using SeleniumSharper.Managers.Enums;
using SeleniumSharper.Managers.Interfaces;

namespace SeleniumSharper.Managers;

public abstract class WebDriverManager<T> : IWebDriverManager<T>
    where T : IWebDriver
{
    private VersionResolveStrategy VersionResolveStrategy { get; set; } = VersionResolveStrategy.LatestVersion;

    private string Version { get; set; } = string.Empty;

    public string Setup()
    {
        var versionToDownload = GetVersion(VersionResolveStrategy, Version);

        var downloadUrl = GetDownloadUrl(versionToDownload);

        var archiveName = Path.GetFileName(downloadUrl);

        var binaryPath = GetBinaryPath(versionToDownload);

        return BinaryUtils.InstallBinary(archiveName, downloadUrl, binaryPath, GetBinaryName());
    }

    public WebDriverManager<T> WithVersion(VersionResolveStrategy versionResolveStrategy)
    {
        VersionResolveStrategy = versionResolveStrategy;

        return this;
    }

    public WebDriverManager<T> WithVersion(string version)
    {
        Version = version;

        return this;
    }

    private string GetBinaryPath(string version)
    {
        var architecture = Environment.Is64BitOperatingSystem ? "64" : "32";

        return Path.Combine(
            Directory.GetCurrentDirectory(),
            "Binaries",
            GetName(),
            version,
            architecture,
            GetBinaryName());
    }

    protected abstract string GetDownloadUrl(string version);

    protected abstract string GetBinaryName();

    protected abstract string GetName();

    protected abstract string GetVersion(VersionResolveStrategy versionResolveStrategy, string? version);
}
