namespace Service.Model.Models.Currency;

public class UserCurrencyRatesResponse
{
   public string From { get; set; }
   public string To { get; set; }
   public decimal Value { get; set; }
   public string BestBank { get; set; }
}
