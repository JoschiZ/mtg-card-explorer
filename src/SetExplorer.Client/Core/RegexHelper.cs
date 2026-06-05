using System.Text.RegularExpressions;

namespace SetExplorer.Client.Core;

public static partial class RegexHelper
{
    [GeneratedRegex("{.+?}")]
    public static partial Regex CardSymbolRegex();
}