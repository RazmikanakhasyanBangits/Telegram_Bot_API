using HtmlAgilityPack;
using htmlWrapDemo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;

namespace DataScrapper
{
    public class AcbaBankDataScrapper : IDataScrapper
    {
        public int Id => 3;

        public IEnumerable<CurrencyModel> Get()
        {
            HttpClient client = new();
            string html = client.GetStringAsync("https://www.acba.am/en")
                .GetAwaiter()
                .GetResult();

            HtmlDocument doc = new();
            doc.LoadHtml(html);
            IEnumerable<HtmlNode> nodes = doc.DocumentNode.SelectNodes("//div[@class='simple_price-row']").Skip(1);

            List<CurrencyModel> models = new();
            foreach (HtmlNode item in nodes)
            {
                HtmlNode[] childs = item.ChildNodes.Where(_ => _.Name == "div").ToArray();

                try
                {
                    CurrencyModel model = new()
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
    }
}
