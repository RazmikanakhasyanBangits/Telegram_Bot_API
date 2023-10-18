﻿using AutoMapper;
using Core.Models;
using DataAccess.Models;

namespace Core.Profiles
{
    public class RatesProfile : Profile
    {
        public RatesProfile()
        {
            _ = CreateMap<RateModel, CurrencyRate>()
                .ForPath(destination => destination.Rates.Buy, source => source.MapFrom(_ => _.BuyValue))
                .ForPath(destination => destination.Rates.Sell, source => source.MapFrom(_ => _.SellValue))
                .ForPath(destination => destination.BaseCurrency, source => source.MapFrom(_ => _.ToCurrency));
        }
    }
}
