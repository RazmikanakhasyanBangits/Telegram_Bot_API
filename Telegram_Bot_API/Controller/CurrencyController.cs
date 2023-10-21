using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Shared.Models.Currency;
using Shared.Models.Rates;
using System.Collections.Generic;

namespace API.Controller
{
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;
        private readonly IBankService _bankService;
        public IConfiguration Configuration { get; }
        public CurrencyController(IBankService bankService, ICurrencyService currencyService)
        {
            _currencyService = currencyService;
            _bankService = bankService;
        }

        [HttpGet("/api/currency/available")]
        public IEnumerable<CurrencyModel> Get()
        {
            return _currencyService.Available();
        }

        [HttpGet("/api/currency/all")]
        public IEnumerable<RatesInfoModel> GetAll()
        {
            return _bankService.AllRates();
        }

    }
}
