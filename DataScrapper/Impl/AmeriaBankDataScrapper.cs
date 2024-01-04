// Ignore Spelling: Ameria Impl

using Core.Helper;
using Core.Services.Implementations;
using Core.Services.Interfaces;
using DataScrapper.Abstraction;
using HtmlAgilityPack;
using htmlWrapDemo;
using Microsoft.Extensions.Configuration;
using Shared.Models.Location;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataScrapper.Impl
{
    public class AmeriaBankDataScrapper : IDataScrapper, ILocation
    {
        private readonly IConfiguration _configuration;
        private readonly ILocationService _locationService;

        public AmeriaBankDataScrapper()
        {

        }
        public AmeriaBankDataScrapper(ILocationService locationService)
        {
            _configuration = new ConfigurationBuilder()
                  .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                  .AddJsonFile("appsettings.json")
                  .Build();
            _locationService = locationService;
        }

        public int Id => 1;
        public IEnumerable<CurrencyModel> Get()
        {
            HttpClient client = new();
            string html = client.GetStringAsync("https://ameriabank.am/")
                .GetAwaiter()
                .GetResult();

            HtmlDocument doc = new();
            doc.LoadHtml(html);
            HtmlNode[] node = doc.DocumentNode.SelectNodes(@"//table[@id='dnn_ctr16862_View_grdRates']//tr")
                .Skip(2).ToArray();
            List<CurrencyModel> models = new();

            foreach (HtmlNode item in node)
            {
                HtmlNode[] tds = item.SelectNodes("td").Take(3).ToArray();
                try
                {
                    CurrencyModel model = new()
                    {
                        Currency = tds[0].InnerText,
                        BuyValue = Convert.ToDecimal(tds[1].InnerText, CultureInfo.InvariantCulture),
                        SellValue = Convert.ToDecimal(tds[2].InnerText, CultureInfo.InvariantCulture),
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

            return nameof(AmeriaBankDataScrapper).Replace("DataScrapper", "");
        }
    }
}
