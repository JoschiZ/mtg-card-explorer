namespace SetExplorer.Scryfall.Models;

using System.Text.Json.Serialization;

public sealed class Price
{
    [JsonPropertyName("eur")]
    public decimal? Eur { get; set; }

    [JsonPropertyName("eur_foil")]
    public decimal? EurFoil { get; set; }

    [JsonPropertyName("tix")]
    public decimal? Tix { get; set; }

    [JsonPropertyName("usd")]
    public decimal? Usd { get; set; }

    [JsonPropertyName("usd_etched")]
    public decimal? UsdEtched { get; set; }

    [JsonPropertyName("usd_foil")]
    public decimal? UsdFoil { get; set; }
}