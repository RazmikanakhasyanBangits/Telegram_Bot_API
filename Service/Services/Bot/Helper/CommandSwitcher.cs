using Core.Services.Implementations;
using Core.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Model.Models.CurrencySettings;
using Service.Services.DataScrapper.Implementation;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Core.Services.Bot.Helper;

public class CommandSwitcher
{
    private readonly IServiceScopeFactory _scopeFactory;
    private IUserActivityHistoryService userActivityHistory;
    private ILocation location;
    private ICurrencySettingService currencySetting;
    private IBankService bankService;
    private static TelegramBotClient Bot;
    private readonly string _token;
    public static Dictionary<string, Func<long, string>> CommandDictionary { get; set; }

    public CommandSwitcher(IServiceScopeFactory scopeFactory, IConfiguration configuration)
    {

        _scopeFactory = scopeFactory;
        CommandDictionary = new Dictionary<string, Func<long, string>>
        {
            {"/all", x=> bankService.GetAll() },
            {"/allbest", x=> bankService.GetAllBest() },
            {"/available", x=> bankService.GetAvailable()},
        };

    }
    public async Task SwitchAsync(Update update, TelegramBotClient Bot)
    {
        using (IServiceScope scope = _scopeFactory.CreateScope())
        {
            IServiceProvider scopedServices = scope.ServiceProvider;
            userActivityHistory = scopedServices.GetRequiredService<IUserActivityHistoryService>();
            bankService = scopedServices.GetRequiredService<IBankService>();
            location = scopedServices.GetRequiredService<ILocation>();
            currencySetting = scopedServices.GetRequiredService<ICurrencySettingService>();
            if (await userActivityHistory.IsUserBlockedAsync(update.Message.Chat.Id))
            {
                await Bot.SendTextMessageAsync(chatId: update.Message.Chat.Id, text: "🚫You Are Currently Blocked🚫");
                return;
            }
            Dictionary<string, Func<Update, Task>> commandActions = new Dictionary<string, Func<Update, Task>>
            {
                    { "/start", async update =>
                        {
                            var buttons = ButtonSettings.ShowButtons();
                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Processing...", replyMarkup: buttons);
                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Done!");
                            await userActivityHistory.AddChatHistory(update, string.Empty);
                        }
                        },
                    { "/getLocation", async update =>
                        {
                            var result = await location.GetLocationsAsync(nameof(AmeriaBankDataScrapper));
                            await Bot.SendTextMessageAsync(update.Message.Chat.Id, result);
                            await userActivityHistory.AddChatHistory(update, result);
                        }
                    },
                    { "GetPairRate",async update =>
                    {
                             var currencies =  update.Message.Caption.Split("-");
                             var amount = Convert.ToDouble(currencies[1].Split(":").Last());
                        _ = await Bot.SendTextMessageAsync(update.Message.Chat.Id, await bankService.BestChange(currencies[0], currencies[1].Split(":").First(), amount));
                        }
                    },
                    { "AddCurrencyConfiguration", async update =>
                        {
                            var currencies = update.Message.Caption.Split("-");
                            await currencySetting.ConfigureUserCurrencySettingsAsync(new ConfigureUserCurrencySettingsRequest
                            {
                                CurrencyFrom = currencies.First(),
                                CurrencyTo = currencies.Last(),
                                UserId = update.Message.From.Id
                            });
                        }
                    },
                    { "/location", async update =>
                        {
                            KeyboardButton request = new KeyboardButton("Փոխանցել") { RequestLocation = true };
                            ReplyKeyboardMarkup replyMarkup = new ReplyKeyboardMarkup(new[] { new[] { request } });
                            await Bot.SendTextMessageAsync(
                            chatId: update.Message.Chat,
                                text: "Փոխանցեք ձեր գտնվելու վայրը սեղմելով ներքևում գտնվող կոճակին:",
                                replyMarkup: replyMarkup);
                            await userActivityHistory.AddChatHistory(update, string.Empty);
                        }
                    }
            };

            if (commandActions.TryGetValue(update.Message.Text, out var action))
            {
                await action(update);
            }
            else if (CommandDictionary.TryGetValue(update.Message.Text, out var function))
            {
                var result = CommandDictionary[update.Message.Text].Invoke(update.Message.Chat.Id);
                await Bot.SendTextMessageAsync(update.Message.Chat.Id, result);
                await userActivityHistory.AddChatHistory(update, result);
            }
            else
            {
                _ = await Bot.SendTextMessageAsync(update.Message.Chat.Id, "Անհասկանալի հրաման, խնդրում ենք մուտքարգել հրամաններից որևիցե մեկը\n/all\n/allBest\n/available");
            }
        }
    }
}
