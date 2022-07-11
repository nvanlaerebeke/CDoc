using System.Text.Json.Serialization;

namespace CDoc.Api.Objects;

public class DocumentInfo
{
    [JsonPropertyName("path")]
    public string Path { get; set; }
    [JsonPropertyName("html")]
    public string Html { get; set; }
}