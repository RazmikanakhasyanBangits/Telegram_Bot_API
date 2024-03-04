using System.Collections.Generic;


namespace Repository.Entity;

public partial class Currency
{
    public Currency()
    {
        RateFromCurrencyNavigations = new HashSet<RateModel>();
        RateToCurrencyNavigations = new HashSet<RateModel>();
    }

    public string Code { get; set; }
    public string Description { get; set; }

    public virtual ICollection<RateModel> RateFromCurrencyNavigations { get; set; }
    public virtual ICollection<RateModel> RateToCurrencyNavigations { get; set; }
}
