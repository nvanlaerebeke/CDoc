using System.Text.Json.Serialization;

namespace Markdown.Api.Objects;

public class Item
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("path")]
    public string Path { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; }
    [JsonPropertyName("hasChildren")]
    public bool HasChildren { get; set; }
}