using Repository.Entity;
using System.Collections.Generic;

namespace Repository.Entity;

public partial class Bank
{
    public Bank()
    {
        Rates = new HashSet<RateModel>();
    }

    public int Id { get; set; }
    public string BankName { get; set; }
    public string BankUrl { get; set; }

    public ICollection<BankLocation> Locations { get; set; }
    public virtual ICollection<RateModel> Rates { get; set; }
}
