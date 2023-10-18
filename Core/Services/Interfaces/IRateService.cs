using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IRateService
    {
        void BulknInsert(IEnumerable<DataAccess.Models.RateModel> currency);
    }
}
