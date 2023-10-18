using AutoMapper;
using Core.Services.Interfaces;
using DataAccess.Repositories.Interfaces;
using Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.Implementations
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
