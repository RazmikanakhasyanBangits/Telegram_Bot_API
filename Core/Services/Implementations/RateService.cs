using Core.Services.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using System.Collections.Generic;

namespace Core.Services.Implementations
{
    public class RateService : IRateService
    {
        private readonly IRatesRepository _dataScrapper;
        public RateService(IRatesRepository dataScrapper)
        {
            _dataScrapper = dataScrapper;
        }

        public void BulkInsert(IEnumerable<RateModel> rate)
        {
            _dataScrapper.BulkInsert(rate);
        }


    }
}
