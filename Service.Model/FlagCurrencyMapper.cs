using System.Collections.Generic;

namespace Service.Model;

public static class FlagCurrencyMapper
{
    private static readonly Dictionary<string, string> FlagToCurrency = new()
    {
        {"AMD", "🇦🇲" },
        {"USD", "🇺🇸" },
        {"EUR", "🇪🇺" },
        {"RUB", "🇷🇺" },
        {"GBP", "🇬🇧" }
    };

    public static string ToFlag(this string currency)
    {
        return FlagToCurrency.ContainsKey(currency) ? FlagToCurrency[currency] : string.Empty;
    }
}
