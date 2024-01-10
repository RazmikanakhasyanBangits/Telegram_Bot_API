using Core.Services.Implementations;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Core.Services.DataScrapper.Abstraction;
using Core.Services.DataScrapper.Impl;
using UserActionsProto;
using Service.Model.Models;
using Repository.Repositories.Implementation;
using Repository;
using Repository.Entity;
using Service.Services.DataScrapper.Implementation;

namespace API.GrpcServer;

public class RateActionsServer : RateActions.RateActionsBase
{
    private readonly IConfiguration _configuration;

    public RateActionsServer(IConfiguration configuration)
    {
        _configuration=configuration;
    }

    public override async Task<Empty> UpdateRates(Empty request, ServerCallContext context)
    {
        string _baseCurrency = _configuration["Configs:BaseCurrency"];

        var list = new List<IDataScrapper>
            {
                new AmeriaBankDataScrapper(),
                new EvocaBankDataScrapper(),
                new AcbaBankDataScrapper(),
                new InecoBankDataScrapper(),
                new UniBankDataScrapper()
            };

        List<ExchangeCurrency> all = new();

        foreach (IDataScrapper scrapper in list)
        {
            try
            {
                IEnumerable<ExchangeCurrency> data = scrapper.Get();
                all.AddRange(data);
            }
            catch
            {
                //ignore
            }
        }
        RateService _service = new(new RatesRepository(new TelegramBotDbContext()));

        await _service.BulkInsert(all.Select(_ => new RateModel
        {
            BankId = _.BankId,
            BuyValue = _.BuyValue,
            SellValue = _.SellValue,
            FromCurrency = _.Currency == "RUR" ? "RUB" : _.Currency,
            ToCurrency = _baseCurrency
        }));



        return new Empty();
    }
}
