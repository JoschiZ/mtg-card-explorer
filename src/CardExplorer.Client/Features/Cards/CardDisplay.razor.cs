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

    private MarkupString? _oracleText;
    private MarkupString? _frontText;
    private MarkupString? _backText;
    private Dictionary<string, ScryfallMtgSymbol>? _symbolLookup;

    protected override async Task OnInitializedAsync()
    {
        if (Card.OracleText?.Contains('{') == true || Card.CardFaces.Any(x => x.OracleText?.Contains('{') ?? false))
        {
            _symbolLookup = await SymbologyClient.GetSymbolLookupAsync(CancellationToken.None);
        }

        _oracleText = GetCardText(Card.OracleText);

        if (Card.HasMultipleFaces)
        {
            _frontText = GetCardText(Card.CardFaces[0].OracleText);
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

        if (_symbolLookup is null)
        {
            return new MarkupString(text);
        }

        var matches = RegexHelper.CardSymbolRegex().Replace(text, ReplaceCardSymbol);

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