using Telegram.Bot.Types.ReplyMarkups;

namespace Service.Services.Bot.Helper;

public static class ButtonSettings
{
    public static ReplyKeyboardMarkup ShowButtons()
    {
        return new(new[]
               {
                 new KeyboardButton("/all"),
                 new KeyboardButton("/allBest"),
                 new KeyboardButton("/available"),
                 new KeyboardButton("/location")
               })
        { ResizeKeyboard = true };
    }
}
