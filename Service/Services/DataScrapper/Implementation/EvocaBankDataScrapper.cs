﻿// Ignore Spelling: Evoca DataImp

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
    public class EvocaBankDataScrapper : IDataScrapper, ILocation
    {
        private readonly IConfiguration _configuration;
        private readonly ILocationService _locationService;
        public EvocaBankDataScrapper()
        {

        }
        public EvocaBankDataScrapper(ILocationService locationService)
        {
            _configuration = new ConfigurationBuilder()
                         .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                         .AddJsonFile("appsettings.json")
                         .Build();
            _locationService = locationService;
        }

        public int Id => 2;

        public IEnumerable<ExchangeCurrency> Get()
        {
            HttpClient client = new();
            string html = client.GetStringAsync("https://www.evoca.am/")
                .GetAwaiter()
                .GetResult();

            //var html = File.ReadAllText("test.html");
            HtmlDocument doc = new();
            doc.LoadHtml(html);
            HtmlNode[] nodes = doc.DocumentNode.SelectNodes("/html/body/main/div[9]/div/div[1]/div/div[1]/div/div[1]/div/div[1]/div/div/table/tbody/tr").ToArray();
            List<ExchangeCurrency> models = new();
            foreach (HtmlNode item in nodes)
            {
                HtmlNode[] tds = item.SelectNodes("td").Take(3).ToArray();

                try
                {
                    ExchangeCurrency model = new()
                    {
                        Currency = tds[0].InnerText.Trim(),
                        BuyValue = Convert.ToDecimal(tds[1].InnerText.Trim(), CultureInfo.InvariantCulture),
                        SellValue = Convert.ToDecimal(tds[2].InnerText.Trim(), CultureInfo.InvariantCulture),
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
            return nameof(EvocaBankDataScrapper).Replace("DataScrapper", "");
        }
    }
}
