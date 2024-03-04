using Service.Model.Models.Currency;
using System.Collections.Generic;

namespace Service.Services.Interfaces;

public interface ICurrencyService
{
    IEnumerable<CurrencyModel> Available();
}
