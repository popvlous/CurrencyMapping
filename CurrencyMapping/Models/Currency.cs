using System.ComponentModel.DataAnnotations;

namespace CurrencyMapping.Models
{
    public class Currency
    {
        [Key]
        public required string code { get; set; }
        //中文名稱 symbol
        public string? cname { get; set; }
        //幣別符號symbol
    }

    public class CurrencyShow : Currency
    {
        //匯率
        public decimal? rate { get; set; }
        //更新時間
        public string? updated { get; set; }
    }
}
