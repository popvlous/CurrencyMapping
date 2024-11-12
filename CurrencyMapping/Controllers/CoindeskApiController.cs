using CurrencyMapping.Data;
using CurrencyMapping.Models;
using CurrencyMapping.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CurrencyMapping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoindeskApiController : ControllerBase
    {
        private readonly CurrencyMappingContext _context;
        private readonly ILogger<CoindeskApiController> _logger;
        private readonly BitcoinPriceService _bitcoinService;
        private readonly IConfiguration _configuration;

        public CoindeskApiController(ILogger<CoindeskApiController> logger, CurrencyMappingContext context, BitcoinPriceService bitcoinService, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _bitcoinService = bitcoinService; 
            _configuration = configuration;
        }

        // GET: api/<CoindeskApiController>
        /// <summary>調用CoindeskApi 並顯示結果</summary>
        /// <returns>
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<BitcoinPriceIndex>> GetBitcoinPrice()
        {
            this._logger.LogInformation("GetBitcoinPrice api start");

            var getDataUrl = $"{_configuration["webApiUrl"]}/v1/bpi/currentprice.json";

            //
            var price = await _bitcoinService.GetBitcoinPriceAsync();

            this._logger.LogInformation("GetBitcoinPrice api response : " + price.ToString());

            return Ok(price);

        }



        /// <summary>調用CoindeskApi解析，並產生新的API</summary>
        /// <returns>
        /// </returns>
        [HttpGet("[action]")]
        public async IAsyncEnumerable<CurrencyShow> CreateCurrenyMappingApi()
        {
            this._logger.LogInformation("CreateCurrenyMappingApi api start");

            var getDataUrl = $"{_configuration["webApiUrl"]}/v1/bpi/currentprice.json";

            //調用CoindeskApi
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(getDataUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            this._logger.LogInformation("調用CoindeskApi response : " + responseBody);

            //解析CoindeskApi response
            JObject json_obj = JObject.Parse(responseBody);
            JObject json_converted = JsonConvert.DeserializeObject<JObject>(responseBody);
            Dictionary<string, string> keyValueMap = new Dictionary<string, string>();
            string isoDateString = json_obj["time"]["updatedISO"].ToString();
            string updated = DateTime.Parse(json_obj["time"]["updatedISO"].ToString()).ToString("yyyy/mm/dd H:mm:ss");

            //返回新的API內容，bpi的key會變動，額外進行處理
            foreach (KeyValuePair<string, JToken> keyValuePair in json_converted)
            {
                if (keyValuePair.Key == "bpi")
                {
                    JObject bpi_j = JsonConvert.DeserializeObject<JObject>(keyValuePair.Value.ToString());
                    foreach (KeyValuePair<string, JToken> keyValuePair_bpi in bpi_j)
                    {
                        var currency = await _context.Currency.FindAsync(keyValuePair_bpi.Key.ToString());
                        yield return new CurrencyShow
                        {
                            code = json_obj["bpi"][keyValuePair_bpi.Key]["code"].ToString(),
                            cname = (currency == null) ? "na" : currency.cname,
                            rate = Convert.ToDecimal(json_obj["bpi"][keyValuePair_bpi.Key]["rate"].ToString()),
                            updated = updated

                        };
                    }
                }
            }

        }
    }
}
