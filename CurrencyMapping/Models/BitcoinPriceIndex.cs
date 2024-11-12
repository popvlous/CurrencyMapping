using System.Text.Json.Serialization;

namespace CurrencyMapping.Models
{
    public class BitcoinPriceIndex
    {
        [JsonPropertyName("time")]
        public TimeInfo Time { get; set; }

        [JsonPropertyName("disclaimer")]
        public string Disclaimer { get; set; }

        [JsonPropertyName("chartName")]
        public string ChartName { get; set; }

        [JsonPropertyName("bpi")]
        public BpiInfo Bpi { get; set; }
    }

    // Time 相關資訊
    public class TimeInfo
    {
        [JsonPropertyName("updated")]
        public string Updated { get; set; }

        [JsonPropertyName("updatedISO")]
        public DateTime UpdatedISO { get; set; }

        [JsonPropertyName("updateduk")]
        public string UpdatedUK { get; set; }
    }

    // BPI (Bitcoin Price Index) 資訊
    public class BpiInfo
    {
        [JsonPropertyName("USD")]
        public CurrencyInfo USD { get; set; }

        [JsonPropertyName("GBP")]
        public CurrencyInfo GBP { get; set; }

        [JsonPropertyName("EUR")]
        public CurrencyInfo EUR { get; set; }
    }

    // 幣別資訊
    public class CurrencyInfo
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("rate")]
        public string Rate { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("rate_float")]
        public decimal RateFloat { get; set; }
    }
}
