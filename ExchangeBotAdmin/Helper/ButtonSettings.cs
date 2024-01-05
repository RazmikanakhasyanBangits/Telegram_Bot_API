using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace ExchangeBotAdmin.Helper
{
    public static class ButtonSettings
    {
        public static ReplyKeyboardMarkup ShowButtons(long chatId, TelegramBotClient sender)
        {
            return new ReplyKeyboardMarkup(new[]
            {
        new[]
        {
            new KeyboardButton("/User"),
            new KeyboardButton("/Block"),
            new KeyboardButton("/Unblock"),
        },
        new[]
        {
            new KeyboardButton("/UpdateRates"),
            new KeyboardButton("/UsersCount"),
            new KeyboardButton("/GetUsers"),
        }
    })
            { ResizeKeyboard = true };
        }
    }
}
