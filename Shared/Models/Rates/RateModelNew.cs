using System;

namespace Shared.Models.Rates
{
    public class RateModelNew
    {
        public int Id { get; set; }
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal BuyValue { get; set; }
        public decimal SellValue { get; set; }

        public int Iteration { get; set; }
        public int BankId { get; set; }
        public DateTime LastUpdated { get; set; }

    }
}
