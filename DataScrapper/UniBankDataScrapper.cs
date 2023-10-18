using HtmlAgilityPack;
using htmlWrapDemo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;

namespace DataScrapper
{
    public class UniBankDataScrapper : IDataScrapper
    {
        public int Id => 5;

        public IEnumerable<CurrencyModel> Get()
        {
            HttpClient client = new();
            string html = client.GetStringAsync("https://www.unibank.am/hy/")
                .GetAwaiter()
                .GetResult();

            HtmlDocument doc = new();
            doc.LoadHtml(html);
            IEnumerable<HtmlNode> nodes = doc.DocumentNode.SelectNodes("/html/body/div/div[2]/section[1]/div/div/div/div[2]/div[1]/div[1]/ul[2]/li")
                .AsEnumerable();
            List<CurrencyModel> models = new();
            while (nodes.GetEnumerator().MoveNext())
            {
                CurrencyModel model = new()
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

    }
}
