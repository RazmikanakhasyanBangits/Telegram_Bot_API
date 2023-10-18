using HtmlAgilityPack;
using htmlWrapDemo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;

namespace DataScrapper
{
    public class AmeriaBankDataScrapper : IDataScrapper
    {
        public int Id => 1;

        public IEnumerable<CurrencyModel> Get()
        {
            HttpClient client = new();
            string html = client.GetStringAsync("https://ameriabank.am/")
                .GetAwaiter()
                .GetResult();

            //var html = File.ReadAllText("test.html");
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
    }
}
