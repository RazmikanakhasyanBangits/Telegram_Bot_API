using DataAccess.Entity;
using System.Collections.Generic;

namespace Core.Services.Interfaces
{
    public interface IRateService
    {
        Task BulkInsert(IEnumerable<RateModel> rate);
    }
}
