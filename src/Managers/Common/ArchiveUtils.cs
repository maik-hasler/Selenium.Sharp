using SharpCompress.Archives;
using SharpCompress.Common;

namespace SeleniumSharper.Managers.Common;

public static class ArchiveUtils
{
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
}