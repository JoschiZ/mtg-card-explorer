using System.Text.RegularExpressions;

namespace CardExplorer.Client.Core;

public static partial class RegexHelper
{
    [GeneratedRegex("{.+?}")]
    public static partial Regex CardSymbolRegex();
}