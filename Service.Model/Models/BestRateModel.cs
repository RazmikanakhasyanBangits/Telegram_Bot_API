using System;

namespace Shared.Models
{
    public class BestRateModel
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public string BestBankForBuying { get; set; }
        public string BestBankForSelling { get; set; }
        public decimal BuyValue { get; set; }
        public decimal SellValue { get; set; }
      
        public DateTime LastUpdated { get; set; }
    }
}
