using Service.Model.Models;
using System.Collections.Generic;

namespace Service.Services.DataScrapper.Abstraction
{
    public interface IDataScrapper
    {
        IEnumerable<ExchangeCurrency> Get();

        int Id { get; }
    }
}