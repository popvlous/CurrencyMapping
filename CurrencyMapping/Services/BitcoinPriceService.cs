using CurrencyMapping.Models;

namespace CurrencyMapping.Services
{
    public class BitcoinPriceService
    {
        private readonly HttpClient _httpClient;

        public BitcoinPriceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BitcoinPriceIndex> GetBitcoinPriceAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<BitcoinPriceIndex>("https://api.coindesk.com/v1/bpi/currentprice.json");
            }
            catch (Exception ex)
            {
                // 適當的錯誤處理
                throw new Exception("Error fetching Bitcoin price", ex);
            }
        }

        // 格式化輸出範例
        public void PrintBitcoinPrice(BitcoinPriceIndex bitcoinPrice)
        {
            Console.WriteLine($"Updated: {bitcoinPrice.Time.UpdatedISO}");
            Console.WriteLine($"Chart Name: {bitcoinPrice.ChartName}");
            Console.WriteLine("\nPrices:");
            Console.WriteLine($"USD: {bitcoinPrice.Bpi.USD.RateFloat:N2}");
            Console.WriteLine($"GBP: {bitcoinPrice.Bpi.GBP.RateFloat:N2}");
            Console.WriteLine($"EUR: {bitcoinPrice.Bpi.EUR.RateFloat:N2}");
        }
    }
}
