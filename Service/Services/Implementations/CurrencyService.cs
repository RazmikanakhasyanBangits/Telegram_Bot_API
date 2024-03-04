using Service.Services.Interfaces;
using Repository.Repositories.Interfaces;
using Repository.Entity;
using Service.Model.Models.Currency;
using System.Collections.Generic;
using System.Linq;

namespace Service.Services.Implementations
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;
        public CurrencyService(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }
        public IEnumerable<CurrencyModel> Available()
        {
            return _currencyRepository.All().Select(Map);
        }
        private CurrencyModel Map(Currency currency)
        {
            return new CurrencyModel
            {
                Code = currency.Code,
                Description = currency.Description
            };
        }
    }
}
