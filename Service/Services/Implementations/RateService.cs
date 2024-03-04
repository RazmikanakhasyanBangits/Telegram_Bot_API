using Service.Services.Interfaces;
using Repository.Entity;
using Repository.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Services.Implementations
{
    public class RateService : IRateService
    {
        private readonly IRatesRepository _dataScrapper;
        public RateService(IRatesRepository dataScrapper)
        {
            _dataScrapper = dataScrapper;
        }

        public async Task BulkInsert(IEnumerable<RateModel> rate)
        {
            await _dataScrapper.BulkInsert(rate);
        }


    }
}
