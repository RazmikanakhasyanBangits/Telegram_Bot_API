using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Service.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class BestRatesController : ControllerBase
    {
        private readonly IBestRateService _bestRates;

        public BestRatesController(IBestRateService bestRates)
        {
            _bestRates = bestRates;
        }

        [HttpGet]
        public async Task<IEnumerable<BestRateModel>> GetBestRates()
        {
            return await _bestRates.GetBestRatesAsync();
        }
    }
}
