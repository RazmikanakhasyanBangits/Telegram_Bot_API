using Service.Model.Models;
using System.Collections.Generic;

namespace Core.Services.DataScrapper.Abstraction
{
    public interface IDataScrapper
    {
        IEnumerable<ExchangeCurrency> Get();

        int Id { get; }
    }
}