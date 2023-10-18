﻿using htmlWrapDemo;
using System.Collections.Generic;

namespace DataScrapper
{
    public interface IDataScrapper
    {
        IEnumerable<CurrencyModel> Get();

        int Id { get; }
    }

}