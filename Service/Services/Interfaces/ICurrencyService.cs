using Service.Model.Models.Currency;
using System.Collections.Generic;

namespace Core.Services.Interfaces;

public interface ICurrencyService
{
    IEnumerable<CurrencyModel> Available();
}
