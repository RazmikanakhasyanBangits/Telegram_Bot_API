﻿using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Repositories.Interfaces
{
    public interface ICurrencyRepository
    {
        public IEnumerable<Currency> All();

    }
}