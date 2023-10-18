
using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Repository
{
   public interface IBankRepository
    {
        public IEnumerable<RateModel> All();
    }
}
