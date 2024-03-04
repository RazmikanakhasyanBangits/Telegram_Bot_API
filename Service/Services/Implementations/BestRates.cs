using AutoMapper;
using Service.Services.Interfaces;
using Repository.Repositories.Interfaces;
using Service.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Services.Implementations
{
    public class BestRateService : IBestRateService
    {
        private readonly IBestRatesRepository _bestRates;
        private readonly IMapper _mapper;
        public BestRateService(IBestRatesRepository bestRates, IMapper mapper)
        {
            _bestRates = bestRates;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BestRateModel>> GetBestRatesAsync()
        {
            IEnumerable<BestRateModel> rates = await _bestRates.GetBestRatesAsync();
            return rates;
        }
    }
}
