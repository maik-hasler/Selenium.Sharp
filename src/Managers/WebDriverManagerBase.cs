using OpenQA.Selenium;
using SeleniumSharper.Managers.Common;
using SeleniumSharper.Managers.Enums;

namespace SeleniumSharper.Managers;

public abstract class WebDriverManagerBase<T> : IWebDriverManager<T>
    where T : IWebDriver
{
    public string Setup(VersionResolveStrategy versionResolveStrategy, string? version)
    {
        var versionToDownload = GetVersion(versionResolveStrategy, version);

        var archiveName = GetArchiveName();

        var downloadUrl = GetDownloadUrl(versionToDownload, archiveName);

        var binaryPath = GetBinaryPath(versionToDownload);

        return BinaryUtils.InstallBinary(archiveName, downloadUrl, binaryPath, GetBinaryName());
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

    protected abstract string GetDownloadUrl(string version, string archiveName);

    protected abstract string GetBinaryName();

    protected abstract string GetName();

    protected abstract string GetArchiveName();

    protected abstract string GetVersion(VersionResolveStrategy versionResolveStrategy, string? version);
}
