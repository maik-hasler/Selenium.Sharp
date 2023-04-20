using SharpCompress.Archives;
using SharpCompress.Common;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SeleniumSharper.Managers;

public abstract class WebDriverManagerBase
{
    protected string InstallBinary()
    {
        var version = GetLatestVersion();

        var fileName = GetFileName();

        var downloadUrl = GetDownloadUrl(version, fileName);

        var temporaryDirectory = CreateTemporaryDirectory();

        var zipPath = Path.Combine(temporaryDirectory.FullName, fileName);

        DownloadBinary(downloadUrl, zipPath);

        var binaryPath = GetBinaryPath(version);

        ExtractArchive(zipPath, binaryPath);

        temporaryDirectory.Delete(true);

        GrantPermissionsIfNeeded(binaryPath);

        return binaryPath;
    }

    private void GrantPermissionsIfNeeded(string binaryPath)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            var chmod = Process.Start("chmod", $"+x {binaryPath}");

            chmod?.WaitForExit();
        }
    }

    private void ExtractArchive(string archivePath, string destinationPath)
    {
        var suffix = Path.GetExtension(archivePath);

        if (suffix.Equals(".exe", StringComparison.OrdinalIgnoreCase))
        {
            File.Copy(archivePath, destinationPath);

            return;
        }

        var binaryName = GetBinaryName();

        if (suffix.Equals(".zip", StringComparison.OrdinalIgnoreCase) || suffix.Equals(".tar.gz", StringComparison.OrdinalIgnoreCase))
        {
            using var archive = ArchiveFactory.Open(archivePath);

            foreach (var entry in archive.Entries.Where(entry => Path.GetFileName(entry.Key).Equals(binaryName, StringComparison.OrdinalIgnoreCase)))
            {
                entry.WriteToDirectory(destinationPath, new ExtractionOptions()
                {
                    ExtractFullPath = true,
                    Overwrite = true
                });
            }
        }
    }

    private DirectoryInfo CreateTemporaryDirectory()
    {
        var temporaryPath = Path.Combine(
            Path.GetTempPath(),
            Guid.NewGuid().ToString());

        return Directory.CreateDirectory(temporaryPath);
    }

    private void DownloadBinary(Uri uri, string path)
    {
        using var httpClient = new HttpClient();

        var response = httpClient.GetAsync(uri).Result;

        response.EnsureSuccessStatusCode();

        using var stream = response.Content.ReadAsStreamAsync().Result;

        using var fileStream = new FileStream(path, FileMode.Create);

        stream.CopyTo(fileStream);
    }

    private string GetBinaryPath(string version)
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

    protected abstract string GetLatestVersion();

    protected abstract Uri GetDownloadUrl(string version, string fileName);

    protected abstract string GetBinaryName();

    protected abstract string GetFileName();
}
