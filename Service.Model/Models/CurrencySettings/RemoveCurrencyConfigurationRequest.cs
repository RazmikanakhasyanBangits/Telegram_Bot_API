namespace Service.Model.Models.CurrencySettings;

public class RemoveCurrencyConfigurationRequest
{
    public string CurrencyFrom { get; set; }
    public string CurrencyTo { get; set; }
    public long UserId { get; set; }
}