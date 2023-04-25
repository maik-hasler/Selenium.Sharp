using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SeleniumSharper.Managers.Common;

public static class BinaryUtils
{
    public static string InstallBinary(string fileName, string downloadUrl, string binaryPath, string binaryName)
    {
        var temporaryDirectory = CreateTemporaryDirectory();

        var archivePath = Path.Combine(temporaryDirectory.FullName, fileName);

        DownloadBinaryArchive(downloadUrl, archivePath);

        Directory.CreateDirectory(binaryPath);

        ArchiveUtils.ExtractArchive(archivePath, binaryPath, binaryName);

        temporaryDirectory.Delete(true);

        GrantPermissionsIfNeeded(binaryPath);

        return binaryPath;
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

    public static void DownloadBinaryArchive(string url, string path)
    {
        using var httpClient = new HttpClient();

        var response = httpClient.GetAsync(url).Result;

        response.EnsureSuccessStatusCode();

        using var stream = response.Content.ReadAsStreamAsync().Result;

        using var fileStream = new FileStream(path, FileMode.Create);

        stream.CopyTo(fileStream);
    }
}
