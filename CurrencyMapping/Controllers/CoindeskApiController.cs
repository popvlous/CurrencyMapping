using CurrencyMapping.Data;
using CurrencyMapping.Models;
using CurrencyMapping.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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

        public CoindeskApiController(ILogger<CoindeskApiController> logger, CurrencyMappingContext context, BitcoinPriceService bitcoinService)
        {
            _context = context;
            _logger = logger;
            _bitcoinService = bitcoinService;
        }

        // GET: api/<CoindeskApiController>
        [HttpGet]
        public async Task<ActionResult<BitcoinPriceIndex>> GetBitcoinPrice()
        {
            this._logger.LogInformation("GetBitcoinPrice api start");
            string webApiUrl = "https://api.coindesk.com";

            var getDataUrl = $"{webApiUrl}/v1/bpi/currentprice.json";

            var price = await _bitcoinService.GetBitcoinPriceAsync();

            return Ok(price);

        }

        [HttpGet("[action]")]
        public async IAsyncEnumerable<CurrencyShow> IAsyncEnumGetNewAPI()
        {

            string webApiUrl = "https://api.coindesk.com";
            var getDataUrl = $"{webApiUrl}/v1/bpi/currentprice.json";

            //HttpRequestMessage request = new(HttpMethod.Get, getDataUrl);

            //var response = await client.SendAsync(request);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(getDataUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            dynamic obj = JsonConvert.DeserializeObject(responseBody, typeof(object));

            JObject json = JObject.Parse(responseBody);
            //bitcoinPrice = (decimal)json["bpi"]["USD"]["rate"];
            JObject converted = JsonConvert.DeserializeObject<JObject>(responseBody);
            Dictionary<string, string> keyValueMap = new Dictionary<string, string>();

            foreach (KeyValuePair<string, JToken> keyValuePair in converted)
            {
                //keyValueMap.Add(keyValuePair.Key, keyValuePair.Value.ToString());
                if (keyValuePair.Key == "bpi")
                {
                    JObject bpi_j = JsonConvert.DeserializeObject<JObject>(keyValuePair.Value.ToString());
                    foreach (KeyValuePair<string, JToken> keyValuePair_bpi in bpi_j)
                    {
                        var currency = await _context.Currency.FindAsync(keyValuePair_bpi.Key.ToString());
                        yield return new CurrencyShow
                        {
                            code = json["bpi"][keyValuePair_bpi.Key]["code"].ToString(),
                            cname = (currency == null) ? "na" : currency.cname,
                            //symbol = json["bpi"][keyValuePair_bpi.Key]["symbol"].ToString(),
                            rate = Convert.ToDecimal(json["bpi"][keyValuePair_bpi.Key]["rate"].ToString()),
                            //description = json["bpi"][keyValuePair_bpi.Key]["symbol"].ToString(),
                            //rate_float = float.Parse(json["bpi"][keyValuePair_bpi.Key]["rate"].ToString())
                        };
                    }
                }
            }

        }
    }
}
