using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Telegram.Bot.Types;

namespace Service.Services.Bot.Helper;

public static class RegexPatternHelper
{
    private static Dictionary<Regex, string> Patterns = new Dictionary<Regex, string>
    {
        { new Regex(@"^[A-Z]{3}-[A-Z]{3}:\d+$"),"GetPairRate"},
        { new Regex(@"^[A-Za-z]{3}\s*-\s*[A-Za-z]{3}$"),"AddCurrencyConfiguration"}
    };

    public static string GetCommandFromText(Update update)
    {
        var command = Patterns.FirstOrDefault(x => x.Key.IsMatch(update.Message.Text));


        if (command.Value == null) return null;
        return command.Value;
    }


}
