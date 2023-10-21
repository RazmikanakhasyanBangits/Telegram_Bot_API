using htmlWrapDemo;
using System.Collections.Generic;

namespace DataScrapper.Abstraction
{
    public interface IDataScrapper
    {
        IEnumerable<CurrencyModel> Get();

        int Id { get; }
    }

}
