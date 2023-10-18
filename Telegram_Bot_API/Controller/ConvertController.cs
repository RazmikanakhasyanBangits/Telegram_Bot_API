using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Infrastructure;
using Shared.Model;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controller
{
    [ApiController]
    [Route("api/convert")]
    public class ConvertController : ControllerBase
    {
        private readonly ICurrencies _currencies;
        private readonly string _baseCurrency;
        public ConvertController(ICurrencies currencies,
            ISettingsProvider settingsProvider)
        {
            _currencies = currencies;
            _baseCurrency = settingsProvider.BaseCurrency;
        }

        [HttpGet("{currency}/all")]
        [SwaggerResponse(200, Type = typeof(IEnumerable<CurrenciesConvertDetails>))]
        public IActionResult All(string currency, double amount)
        {
            return amount == 0
                ? BadRequest("amount not specified")
                : string.IsNullOrEmpty(currency)
                ? BadRequest("currency not specified")
                : Ok(_currencies.GetConvertInfoForAllCurrencies(currency, amount));
        }

        [HttpGet]
        public async Task<IActionResult> Get(string from, string to, decimal amount)
        {
            if (amount == 0)
            {
                return BadRequest("Amount must be specified");
            }

            from = string.IsNullOrEmpty(from) ? "USD" : from;
            to = string.IsNullOrEmpty(to) ? _baseCurrency : to;

            CurrenciesConvertDetails bestRates = await _currencies.ConvertAsync(from, to, amount);
            return Ok(bestRates);
        }

    }
}
