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
        ///   ���թҦ����O��������API
        /// </summary>
        [Test]
        public void Currencies()
        {
            var result = _CurrenciesController.Currencies();

            Assert.That(result, Is.Not.Null);
        }
        /// <summary>
        ///   ���ճ�@���O�त��API
        /// </summary>
        [Test]
        public void GetCurrency()
        {
            string code = "USD";

            var actual= _CurrenciesController.GetCurrency(code);

            var result = new Currency { code = code };

            result.cname = "����";

            Assert.That(actual, Is.Not.Null);
        }

        /// <summary>
        ///   �s�W��@���O�त��API
        /// </summary>
        [Test]
        public void PostCurrency()
        {
            Currency currency = new Currency() { code = "USD" };

            currency.cname = "����";

            var actual = _CurrenciesController.PostCurrency(currency);

            Assert.That(actual, Is.Not.Null);
        }

        /// <summary>
        ///   �R����@���O�त��API
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