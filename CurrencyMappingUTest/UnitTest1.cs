using CurrencyMapping.Controllers;
using CurrencyMapping.Data;
using CurrencyMapping.Models;
using CurrencyMapping.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NUnit.Framework.Constraints;

namespace CurrencyMappingUTest
{
    public class Tests
    {
        private CurrencyMapping.Controllers.CoindeskApiController _CoindeskApiController;
        private readonly CurrencyMappingContext _context;
        private readonly ILogger<CoindeskApiController> _logger;
        private readonly BitcoinPriceService _bitcoinService;
        private readonly IConfiguration _configuration;
        [SetUp]
        public void Setup()
        {
            _CoindeskApiController = new CoindeskApiController(_logger,_context,_bitcoinService,_configuration);
        }


        /// <summary>
        ///   <para>
        /// 測試調用CoindeskAPI</para>
        /// </summary>
        [Test]
        public void GetBitcoinPriceTest()
        {
           var result = _CoindeskApiController.GetBitcoinPrice();

           Assert.That(result, Is.Not.Null);
        }

        /// <summary>
        ///   <para>
        /// 測試調用新API</para>
        /// </summary>
        [Test]
        public void CreateCurrenyMappingApiTest()
        {
            var result = _CoindeskApiController.CreateCurrenyMappingApi();

            Assert.That(result, Is.Not.Null);
        }

    }
}