using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Services.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Shared.Models;

namespace Core
{
   public class RateService : IRateService
    {
        private readonly IRatesRepository _dataScrapper;
        public RateService(IRatesRepository dataScrapper)
        {
            _dataScrapper = dataScrapper;
        }

        public void BulknInsert(IEnumerable<DataAccess.Models.RateModel> rate)
        {
            _dataScrapper.BulkInsert(rate);
        }

       
    }
}
