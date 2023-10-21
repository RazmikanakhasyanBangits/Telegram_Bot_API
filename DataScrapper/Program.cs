using Core.Services.Implementations;
using DataAccess;
using DataAccess.Models;
using DataAccess.Repositories.Implementation;
using DataScrapper.Abstraction;
using DataScrapper.Impl;
using htmlWrapDemo;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataScrapper
{
    public class DataScrapper
    {
        private static List<IDataScrapper> list;

        private static void Main(string[] args)
        {
            string _baseCurrency = JObject.Parse(File.ReadAllText("appsettings.json"))["Configs"]["BaseCurrency"]
                .Value<string>();

            list = new List<IDataScrapper>
            {
                new AmeriaBankDataScrapper(),
                new EvocaBankDataScrapper(),
                new AcbaBankDataScrapper(),
                new InecoBankDataScrapper(),
                new UniBankDataScrapper()
            };

            List<CurrencyModel> all = new();

            foreach (IDataScrapper scrapper in list)
            {
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine($"Getting data from  {scrapper.GetType().Name.Replace("DataScrapper", "")}");
                Console.ForegroundColor = ConsoleColor.Blue;
                try
                {
                    IEnumerable<CurrencyModel> data = scrapper.Get();
                    all.AddRange(data);
                    foreach (CurrencyModel item in data)
                    {
                        Console.WriteLine("Currency: {0} BuyValue: {1} SellValue: {2}", item.Currency, item.BuyValue, item.SellValue);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unable to get data from {scrapper.Id}");
                    Console.WriteLine(ex.Message);
                }
            }

            RateService _service = new(new RatesRepository(new TelegramBotDbContext()));
            _service.BulknInsert(all.Select(_ => new RateModel
            {
                BankId = _.BankId,
                BuyValue = _.BuyValue,
                SellValue = _.SellValue,
                FromCurrency = _.Currency == "RUR" ? "RUB" : _.Currency,
                ToCurrency = _baseCurrency
            }));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Completed!");
            _ = Console.ReadKey();

        }


    }
}
