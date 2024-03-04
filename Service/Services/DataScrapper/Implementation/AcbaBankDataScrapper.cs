// Ignore Spelling: Acba

using Service.Helper;
using Service.Services.DataScrapper.Abstraction;
using Service.Services.Implementations;
using Service.Services.Interfaces;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Service.Model.Models;
using Service.Model.Models.Location;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.DataScrapper.Implementation
{
    public class AcbaBankDataScrapper : IDataScrapper, ILocation
    {
        private readonly IConfiguration _configuration;
        private readonly ILocationService _locationService;
        public AcbaBankDataScrapper()
        {

        }
        public AcbaBankDataScrapper(ILocationService locationService)
        {
            _configuration = new ConfigurationBuilder()
                     .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                     .AddJsonFile("appsettings.json")
                     .Build();
            _locationService = locationService;
        }

        public int Id => 3;

        public IEnumerable<ExchangeCurrency> Get()
        {
            HttpClient client = new();
            string html = client.GetStringAsync("https://www.acba.am/en")
                .GetAwaiter()
                .GetResult();

            HtmlDocument doc = new();
            doc.LoadHtml(html);
            IEnumerable<HtmlNode> nodes = doc.DocumentNode.SelectNodes("//div[@class='simple_price-row']").Skip(1);

            List<ExchangeCurrency> models = new();
            foreach (HtmlNode item in nodes)
            {
                HtmlNode[] childs = item.ChildNodes.Where(_ => _.Name == "div").ToArray();

                try
                {
                    ExchangeCurrency model = new()
                    {
                        Currency = childs[0].InnerText.Trim(),
                        BuyValue = Convert.ToDecimal(childs[1].InnerText.Trim(), CultureInfo.InvariantCulture),
                        SellValue = Convert.ToDecimal(childs[2].InnerText.Trim(), CultureInfo.InvariantCulture),
                        BankId = Id
                    };
                    models.Add(model);
                }
                catch { }

            }
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

            return nameof(AcbaBankDataScrapper).Replace("DataScrapper", "");
        }


    }
}
