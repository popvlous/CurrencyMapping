using Microsoft.VisualStudio.TestTools.UnitTesting;
using CurrencyMapping.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyMapping.Data;
using CurrencyMapping.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using CurrencyMapping.Models;
using NuGet.Protocol;

namespace CurrencyMapping.Controllers.Tests
{
    [TestClass()]
    public class CurrenciesControllerTests
    {
        private readonly CurrencyMappingContext _context;
        private readonly ILogger<CurrencyMappingContext> _logger;
        public CurrenciesControllerTests(CurrencyMappingContext context, ILogger<CurrencyMappingContext> logger)
        {
            _context = context;
            _logger = logger;
        }

        [TestMethod()]
        public void CurrenciesTest()
        {
            CurrenciesController cc = new CurrenciesController(_context, _logger);

            var result = cc.Currencies();

            Assert.IsNotNull(result);
        }
    }
}