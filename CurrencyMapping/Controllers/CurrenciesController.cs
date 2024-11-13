using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CurrencyMapping.Data;
using CurrencyMapping.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Localization;

namespace CurrencyMapping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly CurrencyMappingContext _context;
        private readonly ILogger<CurrencyMappingContext> _logger;
        private readonly IStringLocalizer<CurrenciesController> _localizer;

        public CurrenciesController(CurrencyMappingContext context, ILogger<CurrencyMappingContext> logger, IStringLocalizer<CurrenciesController> localizer)
        {
            _context = context;
            _logger = logger;
            _localizer = localizer;
        }

        // GET: api/Currencies
        /// <summary>獲取所有幣別代碼與中文的對應訊息</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Currency>>> Currencies()
        {
            this._logger.LogInformation("GetAllCurrency api " + _localizer["start"]);
            foreach (var cur in _context.Currency.ToList())
            {
                this._logger.LogInformation("Currency :" + cur.code + cur.cname);

            }
            this._logger.LogInformation("GetAllCurrency api " + _localizer["end"]);
            return await _context.Currency.OrderBy(u => u.code).ToListAsync();
        }

        // GET: api/Currencies/{code}
        /// <summary>獲取個別幣與中文對應</summary>
        /// <param name="code">幣別代碼(需先調用)</param>
        /// <returns>
        /// 
        /// </returns>
        [HttpGet("{code}", Name = nameof(GetCurrency))]
        public async Task<ActionResult<Currency>> GetCurrency(string code)
        {
            this._logger.LogInformation("GetCurrency api " + _localizer["start"]);
            var currency = await _context.Currency.FindAsync(code);

            if (currency == null)
            {
                this._logger.LogInformation(_localizer["currency code is not exist"]);
                return NotFound();
            }

            this._logger.LogInformation("GetCurrency api response log");
            foreach (var cur in _context.Currency.ToList())
            {
                this._logger.LogInformation("Currency :" + cur.code + cur.cname);

            }
            this._logger.LogInformation("GetCurrency api " + _localizer["end"]);
            return currency;
        }

        // PUT: api/Currencies/{code}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>編輯單一幣別中文名稱</summary>
        /// <param name="code">幣別代碼</param>
        /// <param name="currency">更新的Currency 包含 code , cname</param>
        /// <returns>
        /// </returns>
        [HttpPut("{code}")]
        public async Task<IActionResult> PutCurrency(string code, Currency currency)
        {
            this._logger.LogInformation("PutCurrency api " + _localizer["start"]);
            if (code != currency.code)
            {
                this._logger.LogInformation("幣別代碼與更新資料的代碼不符");
                return BadRequest();
            }

            _context.Entry(currency).State = EntityState.Modified;

            this._logger.LogInformation("編輯幣別 : " + code + " 編輯資訊 code :" + currency.code + "編輯資訊 cname :" + currency.cname);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                this._logger.LogInformation("新增幣別時,異常發生:" + ex.Message);
                if (!CurrencyExists(code))
                {
                    this._logger.LogInformation("幣別不存在於資料庫");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            this._logger.LogInformation("PutCurrency api " + _localizer["end"]);

            return NoContent();
        }

        // POST: api/Currencies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>新增幣別資料</summary>
        /// <param name="currency">幣別資料</param>
        /// <returns>
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<Currency>> PostCurrency(Currency currency)
        {
            this._logger.LogInformation("PostCurrency api " + _localizer["start"]);
            _context.Currency.Add(currency);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                this._logger.LogInformation("新增幣別時,異常發生:" + ex.Message);
                if (CurrencyExists(currency.code))
                {
                    this._logger.LogInformation("新增幣別時,幣別代碼已經重複");
                    return Conflict();
                }
                else
                {
                    throw;
                }

            }

            this._logger.LogInformation("PostCurrency api " + _localizer["end"]);

            return CreatedAtRoute("GetCurrency", new { code = currency.code }, currency);
        }

        // DELETE: api/Currencies/5
        /// <summary>刪除幣別代碼與中文名稱的對應資料</summary>
        /// <param name="code">幣別代碼</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteCurrency(string code)
        {
            this._logger.LogInformation("DeleteCurrency api " + _localizer["start"]);
            this._logger.LogInformation("刪除的代碼為 : " + code);
            var currency = await _context.Currency.FindAsync(code);
            if (currency == null)
            {
                return NotFound();
            }

            _context.Currency.Remove(currency);
            await _context.SaveChangesAsync();

            this._logger.LogInformation("DeleteCurrency api "+ _localizer["end"]);

            return NoContent();
        }

        private bool CurrencyExists(string id)
        {
            return _context.Currency.Any(e => e.code == id);
        }
    }
}
