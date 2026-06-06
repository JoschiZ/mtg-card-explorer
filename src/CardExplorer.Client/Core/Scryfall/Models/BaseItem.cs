using System.Text.Json.Serialization;

namespace CardExplorer.Client.Core.Scryfall.Models;

public abstract class BaseItem
{
    [JsonPropertyName("object")]
    public required string ObjectType { get; set; }
}