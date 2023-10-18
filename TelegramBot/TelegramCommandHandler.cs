using Core.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Helper;

namespace TelegramBot
{
    public class TelegramCommandHandler
    {
        private readonly string _token;
        private readonly TelegramBotClient Bot;
        private readonly IBankService bankService;

        public TelegramCommandHandler(IBankService bankService)
        {
            Settings settings = JsonConvert.DeserializeObject<Settings>(System.IO.File.ReadAllText("settings.json"));
            _token = settings.Token;
            Bot = new TelegramBotClient(_token);
            this.bankService = bankService;
        }


        [Obsolete]
        private async void Bot_OnMessage(object sender, MessageEventArgs e)
        {

            if (e.Message.Type == MessageType.Text)
            {
                try
                {
                    string pattern = @"(\d+)([A-Z]{3})([:]([A-Z]{3})){0,1}";
                    Regex regex = new(pattern);
                    if (regex.IsMatch(e.Message.Text))
                    {

                        GroupCollection match = regex.Match(e.Message.Text).Groups;
                        string from = match[2].Value;
                        double amount = double.Parse(match[1].Value);
                        string to = match[4].Value;
                        _ = await Bot.SendTextMessageAsync(e.Message.Chat.Id, await bankService.BestChange(from, to, amount));
                        return;
                    }
                    switch (e.Message.Text)
                    {
                        case "/start":
                            ReplyKeyboardMarkup buttons = ButtonSettings.ShowButtons(e.Message.Chat.Id, (TelegramBotClient)sender);
                            _ = await ((TelegramBotClient)sender).SendTextMessageAsync(
                            e.Message.Chat.Id,
                            "Processing...",
                            replyMarkup: buttons);
                            _ = await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Done!");
                            break;
                        case "/location":
                            KeyboardButton request = new("Send Location") { RequestLocation = true };
                            ReplyKeyboardMarkup replyMarkup = new(new[] { new[] { request } });

                            _ = await ((TelegramBotClient)sender).SendTextMessageAsync(
                                chatId: e.Message.Chat,
                                text: "Share your location by clicking the button below:",
                                replyMarkup: replyMarkup);
                            break;
                        default:
                            string result = CommandSwitcher.CommandDictionary[e.Message.Text].Invoke(e.Message.Chat.Id);
                            _ = await Bot.SendTextMessageAsync(e.Message.Chat.Id, result);
                            break;
                    }

                }
                catch (Exception)
                {
                    _ = await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Անհասկանալի հրաման, խնդրում ենք մուտքարգել հրամաններից որևիցե մեկը\n/all\n/allBest\n/available");
                }

            }
        }

        [Obsolete]
        public void Get()
        {
            Bot.OnMessage += Bot_OnMessage;
            Bot.StartReceiving();
            Bot.OnMessage += async (s, e) =>
            {
                if (e.Message.Type == MessageType.Location)
                {
                    Telegram.Bot.Types.Location userLocation = e.Message.Location;
                    double latitude = userLocation.Latitude;
                    double longitude = userLocation.Longitude;

                    _ = await Bot.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: $"Your location: Latitude: {latitude}, Longitude: {longitude}"
                    );
                    ReplyKeyboardMarkup buttons = ButtonSettings.ShowButtons(e.Message.Chat.Id, Bot);
                    _ = await Bot.SendTextMessageAsync(
                    e.Message.Chat.Id,
                    "Processing...",
                    replyMarkup: buttons);
                    _ = await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Done!");
                }
            };
            _ = Console.ReadLine();
            Bot.StopReceiving();
        }
    }
}
