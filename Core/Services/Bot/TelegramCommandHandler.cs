using Core.Services.Bot.Abstraction;
using Core.Services.Bot.Helper;
using Core.Services.DataScrapper.Impl;
using Core.Services.Implementations;
using Core.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Location = Telegram.Bot.Types.Location;

namespace Core.Services.Bot;

public class TelegramCommandHandler : IHostedService, ICommandHandler
{
    private static string _token;
    private static TelegramBotClient Bot;
    private ReceiverOptions receiverOptions = new();
    private readonly IServiceScopeFactory _scopeFactory;


    public TelegramCommandHandler(IConfiguration configuration, IServiceScopeFactory scopeFactory)
    {
        _token = configuration["token"];
        Bot = new TelegramBotClient(_token);
        _scopeFactory = scopeFactory;
    }

    private async Task Bot_OnLocation(Update update)
    {
        if (update.Message.Type == MessageType.Location)
        {
            using (IServiceScope scope = _scopeFactory.CreateScope())
            {
                IServiceProvider scopedServices = scope.ServiceProvider;
                IBankService bankService = scopedServices.GetRequiredService<IBankService>();
                IUserActivityHistoryService userActivityHistory = scopedServices.GetRequiredService<IUserActivityHistoryService>();


                if (await userActivityHistory.IsUserBlockedAsync(update.Message.Chat.Id))
                {
                    await Bot.SendTextMessageAsync(chatId: update.Message.Chat.Id, text: "🚫You Are Currently Blocked🚫");
                    return;
                }
                Location userLocation = update.Message.Location;
                double latitude = userLocation.Latitude;
                double longitude = userLocation.Longitude;


                var result = await bankService.GetAllBestDistances(latitude, longitude);

                await Bot.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id, text: result);

                ReplyKeyboardMarkup buttons = ButtonSettings.ShowButtons();
                _ = await Bot.SendTextMessageAsync(
                update.Message.Chat.Id,
                "Processing...",
                replyMarkup: buttons);
                _ = await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Done!");
                update.Message.Text = "/locationResponse";
                await userActivityHistory.AddChatHistory(update, result);
            }

        }
    }

    private async Task Bot_OnMessage(Update update)
    {

        if (update.Message.Type == MessageType.Text)
        {
            using (IServiceScope scope = _scopeFactory.CreateScope())
            {
                IServiceProvider scopedServices = scope.ServiceProvider;
                IUserActivityHistoryService userActivityHistory = scopedServices.GetRequiredService<IUserActivityHistoryService>();
                IBankService bankService = scopedServices.GetRequiredService<IBankService>();
                ILocation location = scopedServices.GetRequiredService<ILocation>();
                CommandSwitcher commandSwitcher = scopedServices.GetRequiredService<CommandSwitcher>();


                if (await userActivityHistory.IsUserBlockedAsync(update.Message.Chat.Id))
                {
                    await Bot.SendTextMessageAsync(chatId: update.Message.Chat.Id, text: "🚫You Are Currently Blocked🚫");
                    return;
                }
                try
                {
                    string result = string.Empty;
                    string pattern = @"(\d+)([A-Z]{3})([:]([A-Z]{3})){0,1}";
                    Regex regex = new(pattern);
                    if (regex.IsMatch(update.Message.Text))
                    {

                        GroupCollection match = regex.Match(update.Message.Text).Groups;
                        string from = match[2].Value;
                        double amount = double.Parse(match[1].Value);
                        string to = match[4].Value;
                        _ = await Bot.SendTextMessageAsync(update.Message.Chat.Id, await bankService.BestChange(from, to, amount));
                        return;
                    }
                    switch (update.Message.Text)
                    {
                        case "/start":
                            ReplyKeyboardMarkup buttons = ButtonSettings.ShowButtons();
                            _ = await Bot.SendTextMessageAsync(
                            update.Message.Chat.Id,
                            "Processing...",
                            replyMarkup: buttons);
                            _ = await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Done!");
                            await userActivityHistory.AddChatHistory(update, result);
                            break;
                        case "/getLocation":

                            result = await location.GetLocationsAsync(nameof(AmeriaBankDataScrapper));
                            _ = await Bot.SendTextMessageAsync(update.Message.Chat.Id, result);
                            await userActivityHistory.AddChatHistory(update, result);

                            break;
                        case "/location":
                            KeyboardButton request = new("Փոխանցել") { RequestLocation = true };
                            ReplyKeyboardMarkup replyMarkup = new(new[] { new[] { request } });

                            _ = await Bot.SendTextMessageAsync(
                                chatId: update.Message.Chat,
                                text: "Փոխանցեք ձեր գտնվելու վայրը սեղմելով ներքևում գտնվող կոճակին:",
                                replyMarkup: replyMarkup);
                            await userActivityHistory.AddChatHistory(update, result);

                            break;
                        default:
                            result = CommandSwitcher.CommandDictionary[update.Message.Text].Invoke(update.Message.Chat.Id);
                            _ = await Bot.SendTextMessageAsync(update.Message.Chat.Id, result);
                            await userActivityHistory.AddChatHistory(update, result);

                            break;
                    }
                }
                catch (Exception)
                {
                    _ = await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Անհասկանալի հրաման, խնդրում ենք մուտքարգել հրամաններից որևիցե մեկը\n/all\n/allBest\n/available");
                }
            }
        }
    }

    public void StartBot()
    {
    }

    public void ReStartBot()
    {
    }

    public void StopBot()
    {
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Bot.StartReceiving(
             updateHandler: HandleUpdateAsync,
             pollingErrorHandler: HandlePollingErrorAsync,
             receiverOptions: receiverOptions,
             cancellationToken: CancellationToken.None);

        Bot.GetMeAsync();
        Console.WriteLine($"Start listening for @");
        Console.WriteLine();
        return Task.CompletedTask;
    }

    private async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
    {
        try
        {
            switch (update.Message.Type)
            {
                case MessageType.Location:
                    await Bot_OnLocation(update);
                    break;
                case MessageType.Text:
                    await Bot_OnMessage(update);
                    break;
                default:
                    break;
            }
        }
        catch
        {
            Bot.StartReceiving(
             updateHandler: HandleUpdateAsync,
             pollingErrorHandler: HandlePollingErrorAsync,
             receiverOptions: receiverOptions,
             cancellationToken: CancellationToken.None);

            await Bot.GetMeAsync();
        }
    }

    private async Task HandlePollingErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        await Console.Out.WriteLineAsync(exception.Message);
    }
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
