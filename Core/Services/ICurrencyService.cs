using Shared.Models.Currency;
using System.Collections.Generic;

namespace Core.Services
{
    public interface ICurrencyService
    {
        IEnumerable<CurrencyModel> Available();
    }
}
