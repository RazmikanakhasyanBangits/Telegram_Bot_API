namespace Core.Models
{
    public class RatesModel
    {
        public decimal Buy { get; set; }
        public decimal Sell { get; set; }
    }
    public class CurrencyRate
    {
        public string BaseCurrency { get; set; }
        public RatesModel Rates { get; set; }

    }

}
