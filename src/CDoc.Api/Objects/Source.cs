using System.Text.Json.Serialization;

namespace CDoc.Api.Objects;

/// <summary>
/// The documentation source
/// </summary>
public class Source
{
    /// <summary>
    /// Repository where the source is located
    /// </summary>
    [JsonPropertyName("repository")]
    public string Repository { get; set; } = "";

    /// <summary>
    /// How to cache the source (Disk/Memory)
    /// </summary>
    [JsonPropertyName("cache")]
    public string CacheType { get; set; } = "Disk";

    /// <summary>
    /// Number of seconds between each auto pull
    ///
    /// 0 = disabled
    /// </summary>
    [JsonPropertyName("auto_update_seconds")]
    public int AutoUpdateSeconds { get; set; }

    /// <summary>
    /// Pull before reading a document
    ///
    /// Note: Not Yet Implemented
    /// </summary>
    [JsonPropertyName("pull_before_read")]
    public bool PullBeforeRead { get; set; }
}