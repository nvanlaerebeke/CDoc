using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CDoc.Api.Objects;

/// <summary>
/// Document information for a supported path 
/// </summary>
public class Preview
{
    /// <summary>
    /// Name of the document
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; } = "";
    
    /// <summary>
    /// Path of the document
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    [JsonPropertyName("path")]
    public string Path { get; set; } = "";
   
    /// <summary>
    /// Html that represents the document
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    [JsonPropertyName("html")]
    public string Html { get; set; } = "";
}