// Ignore Spelling: Uni DataImp

using Core.Helper;
using Core.Services.DataScrapper.Abstraction;
using Core.Services.Implementations;
using Core.Services.Interfaces;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Shared.Models;
using Shared.Models.Location;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.DataScrapper.Impl
{
    public class UniBankDataScrapper : IDataScrapper, ILocation
    {
        private readonly IConfiguration _configuration;
        private readonly ILocationService _locationService;
        public UniBankDataScrapper()
        {

        }
        public UniBankDataScrapper(ILocationService locationService)
        {
            _configuration = new ConfigurationBuilder()
                         .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                         .AddJsonFile("appsettings.json")
                         .Build();
            _locationService = locationService;
        }
        public int Id => 5;

        public IEnumerable<ExchangeCurrency> Get()
        {
            HttpClient client = new();
            string html = client.GetStringAsync("https://www.unibank.am/hy/")
                .GetAwaiter()
                .GetResult();

            HtmlDocument doc = new();
            doc.LoadHtml(html);
            IEnumerable<HtmlNode> nodes = doc.DocumentNode.SelectNodes("/html/body/div/div[2]/section[1]/div/div/div/div[2]/div[1]/div[1]/ul[2]/li")
                .AsEnumerable();
            List<ExchangeCurrency> models = new();
            while (nodes.GetEnumerator().MoveNext())
            {
                ExchangeCurrency model = new()
                {
                    Currency = nodes.ElementAt(0).InnerText.Trim(),
                    BuyValue = Convert.ToDecimal(nodes.ElementAt(1).InnerText.Trim(), CultureInfo.InvariantCulture),
                    SellValue = Convert.ToDecimal(nodes.ElementAt(2).InnerText.Trim(), CultureInfo.InvariantCulture),
                    BankId = Id
                };
                models.Add(model);
                nodes = nodes.Skip(3);
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

            return nameof(UniBankDataScrapper).Replace("DataScrapper", "");
        }
    }
}
