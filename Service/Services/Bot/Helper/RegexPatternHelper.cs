using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Telegram.Bot.Types;

namespace Service.Services.Bot.Helper;

public static class RegexPatternHelper
{
    private static Dictionary<Regex, string> Patterns = new Dictionary<Regex, string>
    {
        { new Regex(@"^[A-Z]{3}-[A-Z]{3}:\d+$"),"GetPairRate"},
        { new Regex(@"^[A-Za-z]{3}\s*-\s*[A-Za-z]{3}$"),"AddCurrencyConfiguration"},
        { new Regex(@"^[A-Za-z]{3}-[A-Za-z]{3}:X$"),"RemoveCurrencyConfiguration"},
    };

    public static string GetCommandFromText(Update update)
    {
        var command = Patterns.FirstOrDefault(x => x.Key.IsMatch(update.Message.Text));


        if (command.Value == null) return null;
        return command.Value;
    }

    public static string GenerateCurrencyPairHelpGuidance()
    {
        var response = new StringBuilder();


        response.AppendLine("<i>To find the exchange rate for converting from <b>USD</b> (your starting currency) to <b>AMD</b> (your target currency), simply enter the amount you want to convert (e.g., <b>100 USD</b>)</i>");
        response.AppendLine("✔️ USD-AMD:<b>100</b>");
        response.AppendLine("✔️ EUR-USD:<b>150</b>\n");
        response.AppendLine("                                                                                                                                                                       ");
        response.AppendLine("To add currency pairs to your configuration, specify the starting currency (<b>USD</b>) and the target currency (<b>AMD</b>)");
        response.AppendLine("✔️ <b>USD</b>-<b>AMD</b>");
        response.AppendLine("✔️ <b>EUR</b>-<b>USD</b>\n");

        response.AppendLine("                                                                                                              ");
        response.AppendLine("To remove currency pairs from your configuration, specify the starting currency (<b>USD</b>) and the target currency (<b>AMD</b>), followed by <b>\":X\"</b> to indicate removal");
        response.AppendLine("✔️ <b>USD</b>-<b>AMD</b>:<b>X</b>");
        response.AppendLine("✔️ <b>EUR</b>-<b>USD</b>:<b>X</b>\n");
        response.AppendLine("                                                                                                                                                           ");

        return response.ToString();
    }

}
