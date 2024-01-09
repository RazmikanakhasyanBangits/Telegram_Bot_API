using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Core.Services.Bot.Helper
{
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
}
