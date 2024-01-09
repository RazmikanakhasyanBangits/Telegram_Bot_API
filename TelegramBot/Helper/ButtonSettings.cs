using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Helper
{
    public static class ButtonSettings
    {
        public static ReplyKeyboardMarkup ShowButtons(long chatId, TelegramBotClient sender)
        {
            return new ReplyKeyboardMarkup();
        }
    }
}
