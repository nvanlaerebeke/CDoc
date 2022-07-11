using System.Text.Json.Serialization;

namespace CDoc.Config;

public class Source
{
    [JsonPropertyName("ssh_key")]
    public string SshKey { get; set; } = "";

    [JsonPropertyName("repository")]
    public string Repository { get; set; } = "";

    [JsonPropertyName("cache")]
    public string CacheType { get; set; } = "Disk";

    [JsonPropertyName("auto_update_seconds")]
    public int AutoUpdateSeconds { get; set; } = 0;

    [JsonPropertyName("pull_before_read")]
    public bool PullBeforeRead { get; set; } = false;
}