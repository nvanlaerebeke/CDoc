using System.Text.Json.Serialization;

namespace CDoc.Config;

public class CDocConfig
{
    [JsonPropertyName("sources")]
    public IEnumerable<Source>? Sources { get; set; }
}