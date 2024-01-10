﻿using Core.Services.Implementations;
using Core.Services.Interfaces;
using Repository.Repositories.Implementation;
using DataScrapper.Impl;
using ExchangeBot.Abstraction;
using Microsoft.Extensions.Configuration;
using Shared.Models.Location;
using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bots.Requests;
using TelegramBot.Helper;

namespace ExchangeBot
{
    public class TelegramCommandHandler : ICommandHandler
    {
        private static string _token;
        private static TelegramBotClient Bot;
        private readonly IBankService bankService;
        private readonly ILocation location;
        private readonly IUserActivityHistoryService userActivityHistory;

        public TelegramCommandHandler(IBankService bankService, IConfiguration configuration, ILocation location,
            IUserActivityHistoryService userActivityHistory)
        {
            _token= configuration["token"];
            Bot= new TelegramBotClient(_token);
            this.bankService = bankService;
            this.location = location;
            this.userActivityHistory = userActivityHistory;
        }

        private async void Bot_OnLocation(object sender, MessageEventArgs e)
        {
            if (e.Message.Type == MessageType.Location)
            {
                if (await userActivityHistory.IsUserBlockedAsync(e.Message.Chat.Id))
                {
                    await Bot.SendTextMessageAsync(chatId: e.Message.Chat.Id, text: "🚫You Are Currently Blocked🚫");
                    return;
                }
                Telegram.Bot.Types.Location userLocation = e.Message.Location;
                double latitude = userLocation.Latitude;
                double longitude = userLocation.Longitude;
                string result = await bankService.GetAllBestDistances(latitude, longitude);
                _ = await Bot.SendTextMessageAsync(
                    chatId: e.Message.Chat.Id,
                    text: result
                );
                ReplyKeyboardMarkup buttons = ButtonSettings.ShowButtons(e.Message.Chat.Id, Bot);
                _ = await Bot.SendTextMessageAsync(
                e.Message.Chat.Id,
                "Processing...",
                replyMarkup: null);
                _ = await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Done!");
                e.Message.Text = "/locationResponse";
                await userActivityHistory.AddChatHistory(e, result);
            }
        }

        [Obsolete]
        private async void Bot_OnMessage(object sender, MessageEventArgs e)
        {

            if (e.Message.Type == MessageType.Text)
            {
                if (await userActivityHistory.IsUserBlockedAsync(e.Message.Chat.Id))
                {
                    await Bot.SendTextMessageAsync(chatId: e.Message.Chat.Id, text: "🚫You Are Currently Blocked🚫");
                    return;
                }
                try
                {
                    string result = string.Empty;
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
                    switch (e.Message.Text.ToLower())
                    {
                        case "/start":
                            _ = await Bot.SendTextMessageAsync(e.Message.Chat.Id, "How Can I Help You?");
                            await userActivityHistory.AddChatHistory(e, result);
                            break;
                        case "/getlocation":

                            result = await location.GetLocationsAsync(nameof(AmeriaBankDataScrapper));
                            _ = await Bot.SendTextMessageAsync(e.Message.Chat.Id, result);
                            await userActivityHistory.AddChatHistory(e, result);

                            break;
                        case "/location":
                            KeyboardButton request = new("Փոխանցել") { RequestLocation = true };
                            ReplyKeyboardMarkup replyMarkup = new(new[] { new[] { request } }, true, true);

                            _ = await ((TelegramBotClient)sender).SendTextMessageAsync(
                                chatId: e.Message.Chat,
                                text: "Փոխանցեք ձեր գտնվելու վայրը սեղմելով ներքևում գտնվող կոճակին:",
                                replyMarkup: replyMarkup);
                            await userActivityHistory.AddChatHistory(e, result);

                            break;
                        default:
                            result = CommandSwitcher.CommandDictionary[e.Message.Text].Invoke(e.Message.Chat.Id);
                            _ = await Bot.SendTextMessageAsync(e.Message.Chat.Id, result);
                            await userActivityHistory.AddChatHistory(e, result);

                            break;
                    }
                }
                catch (Exception)
                {
                    _ = await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Անհասկանալի հրաման, խնդրում ենք մուտքարգել հրամաններից որևիցե մեկը\n/all\n/allBest\n/available");
                }

            }
        }

        public void StartBot()
        {
            Bot.OnMessage += Bot_OnMessage;
            Bot.StartReceiving();
            Bot.OnMessage += Bot_OnLocation;
            _ = Console.ReadLine();
            Bot.StopReceiving();
        }

        public void ReStartBot()
        {
            Bot.OnMessage += Bot_OnMessage;
            Bot.OnMessage += Bot_OnLocation;
            Bot.StartReceiving();
            _ = Console.ReadLine();
            Bot.StopReceiving();
        }

        public void StopBot()
        {
            Bot.OnMessage += Bot_OnMessage;
            Bot.OnMessage += Bot_OnLocation;
            Bot.StartReceiving();
            _ = Console.ReadLine();
            Bot.StopReceiving();
        }

    }
}
