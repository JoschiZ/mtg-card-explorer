namespace SetExplorer.Scryfall.Models;

using System.Text.Json.Serialization;


public abstract class BaseItem
{
    [JsonPropertyName("object")]
    public required string ObjectType { get; set; }
}