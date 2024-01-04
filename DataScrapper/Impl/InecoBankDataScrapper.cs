// Ignore Spelling: Ineco Online

using Core.Helper;
using Core.Services.Implementations;
using Core.Services.Interfaces;
using DataScrapper.Abstraction;
using htmlWrapDemo;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shared.Models.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataScrapper.Impl;
public class InecoBankDataScrapper : IDataScrapper, ILocation
{
    private readonly IConfiguration _configuration;
    private readonly ILocationService _locationService;
    public InecoBankDataScrapper()
    {

    }
    public InecoBankDataScrapper(ILocationService locationService)
    {
        _configuration = new ConfigurationBuilder()
                         .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                         .AddJsonFile("appsettings.json")
                         .Build();
        _locationService = locationService;
    }


    public int Id => 4;

    public IEnumerable<CurrencyModel> Get()
    {
        IEnumerable<CurrencyModel> models = null;
        HttpClient client = new();
        try
        {
            string json = client.GetStringAsync("https://www.inecobank.am/api/rates/")
                .GetAwaiter()
                .GetResult();
            models = JsonConvert.DeserializeObject<InecoDataModel>(json).Items
                .Where(_ => _.Card.Buy.HasValue && _.Card.Sell.HasValue)
                .Select(_ => new CurrencyModel
                {
                    BuyValue = (decimal)_.Card.Buy,
                    SellValue = (decimal)_.Card.Sell,
                    Currency = _.Code,
                    BankId = Id
                });

        }
        catch { }

        return models;
    }

    public async Task<string> GetLocationsAsync(string selection)
    {
        GetLocationResponseModel data = await LocationHelper.GetLocationsByNameAsync(new GetLocationRequestModel
        {
            ApiKey = _configuration["Location:ApiKey"],
            BaseUrl = _configuration["Location:BaseUrl"],
            LocationName = ToString()
        });
        await _locationService.AddBankLocation(data, selection);
        StringBuilder builder = new();
        foreach (Result item in data.Results)
        {
            _ = builder.AppendLine($"{item.Formatted_address}");

            _ = builder.AppendLine($"Թարմացվել է` {DateTime.UtcNow:G}");
            _ = builder.AppendLine("--------------------------------------------");
        }
        return builder.ToString();
    }

    public override string ToString()
    {
        return nameof(InecoBankDataScrapper).Replace("DataScrapper", "");
    }


    public partial class InecoDataModel
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("cash")]
        public Card Cash { get; set; }

        [JsonProperty("cashless")]
        public Card Cashless { get; set; }

        [JsonProperty("online")]
        public Card Online { get; set; }

        [JsonProperty("cb")]
        public Card Cb { get; set; }

        [JsonProperty("card")]
        public Card Card { get; set; }
    }

    public partial class Card
    {
        [JsonProperty("buy")]
        public double? Buy { get; set; }

        [JsonProperty("sell")]
        public double? Sell { get; set; }
    }
}
