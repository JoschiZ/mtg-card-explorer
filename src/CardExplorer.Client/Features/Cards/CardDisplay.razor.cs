using System.Text.RegularExpressions;
using CardExplorer.Client.Core;
using CardExplorer.Client.Core.Scryfall.Models;
using CardExplorer.Client.Core.Scryfall.Symbology.Models;
using Microsoft.AspNetCore.Components;

namespace CardExplorer.Client.Features.Cards;

public partial class CardDisplay
{
    [Parameter, EditorRequired] public required ScryfallCard Card { get; set; }

    [Parameter] public bool IsSeen { get; set; }

    private MarkupString? _manaCost;
    private MarkupString? _oracleText;
    
    private MarkupString? _frontManaCost;
    private MarkupString? _frontText;
    private MarkupString? _backManaCost;
    private MarkupString? _backText;
    private Dictionary<string, ScryfallMtgSymbol>? _symbolLookup;

    protected override async Task OnInitializedAsync()
    {
        _symbolLookup = await SymbologyClient.GetSymbolLookupAsync(CancellationToken.None);

        _oracleText = GetCardText(Card.OracleText);
        _manaCost = GetCardText(Card.ManaCost);

        if (Card.HasMultipleFaces)
        {
            _frontManaCost = GetCardText(Card.CardFaces[0].ManaCost);
            _frontText = GetCardText(Card.CardFaces[0].OracleText);
            _backManaCost = GetCardText(Card.CardFaces[1].ManaCost);
            _backText = GetCardText(Card.CardFaces[1].OracleText);
        }


        await base.OnInitializedAsync();
    }

    private MarkupString? GetCardText(string? text)
    {
        if (text is null)
        {
            return null;
        }

        var oracleSegments = text
            .Replace("(", "<i>(")
            .Replace(")", ")</i>")
            .Split('\n')
            .Select(x => $"<p>{x}</p>");

        var oracleHtml = string.Join("\n", oracleSegments);
        
        
        if (_symbolLookup is null)
        {
            return new MarkupString(oracleHtml);
        }

        var matches = RegexHelper.CardSymbolRegex().Replace(oracleHtml, ReplaceCardSymbol);

        return new MarkupString(matches);

        string ReplaceCardSymbol(Match match)
        {
            if (!_symbolLookup.TryGetValue(match.Value, out var symbol))
            {
                return match.Value;
            }

            return $"""<abbr title="{symbol.Description}" class="card-symbol" style="background-image: url('{symbol.SvgUri}')">{symbol.Symbol}</abbr>""";
        }
    }
}