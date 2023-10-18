using htmlWrapDemo;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace DataScrapper;
public class InecoBankDataScrapper : IDataScrapper
{
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
