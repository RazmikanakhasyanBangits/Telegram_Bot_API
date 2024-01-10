using AutoMapper;
using Repository.Entity;
using Service.Model.Models.Location;
using Service.Model.Models.Rates;

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

            _ = CreateMap<BankLocationResponseModel, BankLocation>().ReverseMap();
        }
    }
}

