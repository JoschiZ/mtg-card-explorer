using System.Text.Json.Serialization;
using SetExplorer.Client.Core.Scryfall.Models;

namespace SetExplorer.Client.Core.Scryfall.Symbology.Models;

public class ScryfallMtgSymbol : BaseItem
{
    [JsonPropertyName("symbol")]
    public required string Symbol { get; init; }
    [JsonPropertyName("svg_uri")]
    public required Uri SvgUri { get; init; }
    [JsonPropertyName("english")]
    public string Description { get; init; } = "";
}