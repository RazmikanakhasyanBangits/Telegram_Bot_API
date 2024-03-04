using Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Service.Model.Models;
using Service.Model.Models.Rates;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.TelegramBot.Controller;

[ApiController]
[Route("api/convert")]
public class ConvertController : ControllerBase
{
    private readonly ICurrencies _currencies;

    public ConvertController(ICurrencies currencies)
    {
        _currencies = currencies;
    }

    [HttpGet("{currency}/all")]
    [SwaggerResponse(200, Type = typeof(List<CurrenciesConvertDetails>))]
    public async Task<IActionResult> AllRates([FromQuery] GetAllRatesRequestModel model)
    {
        return Ok(await _currencies.GetConvertInfoForAllCurrencies(model.Currency, model.Amount));
    }

    [HttpGet]
    [SwaggerResponse(200, Type = typeof(CurrenciesConvertDetails))]
    public async Task<IActionResult> Convert([FromQuery] ConvertCurrencyRequestModel request)
    {
        return Ok(await _currencies.ConvertAsync(request.From, request.To, request.Amount));
    }
}
