using HtmlAgilityPack;
using htmlWrapDemo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;

namespace DataScrapper
{
    public class EvocaBankDataScrapper : IDataScrapper
    {
        public int Id => 2;

        public IEnumerable<CurrencyModel> Get()
        {
            HttpClient client = new();
            string html = client.GetStringAsync("https://www.evoca.am/")
                .GetAwaiter()
                .GetResult();

            //var html = File.ReadAllText("test.html");
            HtmlDocument doc = new();
            doc.LoadHtml(html);
            HtmlNode[] nodes = doc.DocumentNode.SelectNodes("/html/body/main/div[9]/div/div[1]/div/div[1]/div/div[1]/div/div[1]/div/div/table/tbody/tr").ToArray();
            List<CurrencyModel> models = new();
            foreach (HtmlNode item in nodes)
            {
                HtmlNode[] tds = item.SelectNodes("td").Take(3).ToArray();

                try
                {
                    CurrencyModel model = new()
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
    }
}
