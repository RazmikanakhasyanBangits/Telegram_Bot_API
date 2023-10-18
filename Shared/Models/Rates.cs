using System;

namespace Shared.Models
{
    public class Rate
    {
        public int ID { get; set; }
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal BuyValue { get; set; }
        public decimal SellValue { get; set; }
        public int BankId { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
