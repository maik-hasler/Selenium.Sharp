using OpenQA.Selenium;
using SeleniumSharper.Managers.Common;
using SeleniumSharper.Managers.Enums;
using SeleniumSharper.Managers.Interfaces;

namespace SeleniumSharper.Managers;

public abstract class WebDriverManagerBase<T> : IWebDriverManager<T>
    where T : IWebDriver
{
    private readonly WebDriverManagerConfiguration _configuration = new();

    public string Setup()
    {
        var versionToDownload = GetVersion(_configuration.VersionResolveStrategy, _configuration.Version);

        var downloadUrl = GetDownloadUrl(versionToDownload);

        var archiveName = Path.GetFileName(downloadUrl);

        var binaryPath = GetBinaryPath(versionToDownload);

        var driverPath = BinaryUtils.InstallBinary(archiveName, downloadUrl, binaryPath, GetBinaryName());

        if (_configuration.AddToPath)
        {
            SetPathEnviromentVariable(driverPath);
        }

        return driverPath;
    }

    private static void SetPathEnviromentVariable(string path)
    {
        if (!Directory.Exists(path))
        {
            throw new ArgumentException("Invalid path");
        }

        const string pathVariableName = "PATH";

        var pathVariable = Environment.GetEnvironmentVariable(pathVariableName, EnvironmentVariableTarget.Process)
            ?? throw new ArgumentNullException(pathVariableName, $"Can't get {pathVariableName} variable");

        if (pathVariable.Contains(path))
        {
            return;
        }

        var newPathVariable = Path.Combine(path, pathVariable);

        Environment.SetEnvironmentVariable(pathVariableName, newPathVariable, EnvironmentVariableTarget.Process);
    }

    public WebDriverManagerBase<T> With(Action<WebDriverManagerConfiguration> configure)
    {
        configure(_configuration);

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
