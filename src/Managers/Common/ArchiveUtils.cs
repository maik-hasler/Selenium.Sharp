using SharpCompress.Archives;
using SharpCompress.Common;

namespace SeleniumSharper.Managers.Common;

public static class ArchiveUtils
{
    public static void ExtractArchive(string archivePath, string destinationPath, string binaryName)
    {
        var suffix = Path.GetFileNameWithoutExtension(archivePath);

        if (suffix.Equals(".exe", StringComparison.OrdinalIgnoreCase))
        {
            File.Copy(archivePath, destinationPath, true);

            return;
        }

        using var archive = ArchiveFactory.Open(archivePath);

        var entry = FindEntryInArchive(archive, binaryName);

        entry.WriteToDirectory(destinationPath, new ExtractionOptions()
        {
            ExtractFullPath = true,
            Overwrite = true
        });
    }

    private static IArchiveEntry FindEntryInArchive(IArchive archive, string binaryName)
    {
        return archive.Entries.FirstOrDefault(e =>
            Path.GetFileNameWithoutExtension(e.Key)
            .Equals(binaryName, StringComparison.OrdinalIgnoreCase))
            ?? throw new Exception($"Binary {binaryName} not found in archive.");
    }
}
