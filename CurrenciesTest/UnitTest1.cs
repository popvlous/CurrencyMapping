using CurrencyMapping.Controllers;
using CurrencyMapping.Data;
using CurrencyMapping.Models;
using Microsoft.Extensions.Logging;

namespace CurrenciesTest
{
    public class Tests
    {
        private CurrencyMapping.Controllers.CurrenciesController _CurrenciesController;
        private readonly CurrencyMappingContext _context;
        private readonly ILogger<CurrencyMappingContext> _logger;

        [SetUp]
        public void Setup()
        {
            _CurrenciesController = new CurrenciesController(_context, _logger);
        }


        /// <summary>
        ///   測試所有幣別對應中文API
        /// </summary>
        [Test]
        public void Currencies()
        {
            var result = _CurrenciesController.Currencies();

            Assert.That(result, Is.Not.Null);
        }
        /// <summary>
        ///   測試單一幣別轉中文API
        /// </summary>
        [Test]
        public void GetCurrency()
        {
            string code = "USD";

            var actual= _CurrenciesController.GetCurrency(code);

            var result = new Currency { code = code };

            result.cname = "美金";

            Assert.That(actual, Is.Not.Null);
        }

        /// <summary>
        ///   新增單一幣別轉中文API
        /// </summary>
        [Test]
        public void PostCurrency()
        {
            Currency currency = new Currency() { code = "USD" };

            currency.cname = "美金";

            var actual = _CurrenciesController.PostCurrency(currency);

            Assert.That(actual, Is.Not.Null);
        }

        /// <summary>
        ///   刪除單一幣別轉中文API
        /// </summary>
        [Test]
        public void DeleteCurrency()
        {
            string code = "USD";

            var actual = _CurrenciesController.DeleteCurrency(code);

            Assert.That(actual, Is.Not.Null);
        }
    }
}