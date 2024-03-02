using Core.Services.Bot.Abstraction;
using Core.Services.Bot.Helper;
using Core.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.Services.Bot.Helper;
using System;
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
        _scopeFactory = scopeFactory;
        _token = configuration["token"];
        Bot = new TelegramBotClient(_token);
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
                CommandSwitcher commandSwitcher = scopedServices.GetRequiredService<CommandSwitcher>();

                var regexResult = RegexPatternHelper.GetCommandFromText(update);
                update.Message.Caption = update.Message.Text;
                update.Message.Text = regexResult is null?update.Message.Text : regexResult;
                await commandSwitcher.SwitchAsync(update,Bot);
            }
        }
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
