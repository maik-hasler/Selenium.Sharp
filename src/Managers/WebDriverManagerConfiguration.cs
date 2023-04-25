using SeleniumSharper.Managers.Enums;

namespace SeleniumSharper.Managers;

public class WebDriverManagerConfiguration
{
    public VersionResolveStrategy VersionResolveStrategy { get; set; } = VersionResolveStrategy.LatestVersion;

    public string Version { get; set; } = string.Empty;

    public bool AddToPath { get; set; }
}
