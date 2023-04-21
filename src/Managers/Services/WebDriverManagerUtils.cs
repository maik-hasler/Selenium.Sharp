using SharpCompress.Archives;
using SharpCompress.Common;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SeleniumSharper.Managers.Services;

public static class WebDriverManagerUtils
{
    public static string InstallBinary(string fileName, Uri downloadUrl, string binaryPath, string binaryName)
    {
        var temporaryDirectory = WebDriverManagerUtils.CreateTemporaryDirectory();

        var archivePath = Path.Combine(temporaryDirectory.FullName, fileName);

        WebDriverManagerUtils.DownloadBinary(downloadUrl, archivePath);

        Directory.CreateDirectory(binaryPath);

        WebDriverManagerUtils.ExtractArchive(archivePath, binaryPath, binaryName);

        temporaryDirectory.Delete(true);

        WebDriverManagerUtils.GrantPermissionsIfNeeded(binaryPath);

        return binaryPath;
    }

    public static void ExtractArchive(string archivePath, string destinationPath, string binaryName)
    {
        var suffix = Path.GetExtension(archivePath);

        if (suffix.Equals(".exe", StringComparison.OrdinalIgnoreCase))
        {
            File.Copy(archivePath, destinationPath);

            return;
        }

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

    public static void GrantPermissionsIfNeeded(string binaryPath)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            var chmod = Process.Start("chmod", $"+x {binaryPath}");

            chmod?.WaitForExit();
        }
    }

    public static DirectoryInfo CreateTemporaryDirectory()
    {
        var temporaryPath = Path.Combine(
            Path.GetTempPath(),
            Guid.NewGuid().ToString());

        return Directory.CreateDirectory(temporaryPath);
    }

    public static void DownloadBinary(Uri uri, string path)
    {
        using var httpClient = new HttpClient();

        var response = httpClient.GetAsync(uri).Result;

        response.EnsureSuccessStatusCode();

        using var stream = response.Content.ReadAsStreamAsync().Result;

        using var fileStream = new FileStream(path, FileMode.Create);

        stream.CopyTo(fileStream);
    }
}
