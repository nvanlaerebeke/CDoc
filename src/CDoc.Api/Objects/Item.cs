using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CDoc.Api.Objects;

/// <summary>
/// Represents item in the document tree
/// </summary>
public class Item
{
    /// <summary>
    /// Name of the document
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    /// <summary>
    /// Relative path in the document tree 
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    [JsonPropertyName("path")]
    public string Path { get; set; } = "";

    /// <summary>
    /// Kind of document
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    [JsonPropertyName("mimetype")]
    public string MimeType { get; set; } = "";
    
    /// <summary>
    /// Does the API provide an HTML preview for this type
    /// </summary>
    [Required]
    [JsonPropertyName("hasPreview")]
    public bool HasPreview { get; set; }
    
    /// <summary>
    /// Size in bytes of the item
    ///
    /// 0 for directories
    /// </summary>
    [Required]
    [JsonPropertyName("size")]
    public int Size { get; set; }
}