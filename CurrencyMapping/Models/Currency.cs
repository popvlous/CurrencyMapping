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
        //中文名稱 symbol
        public decimal? rate { get; set; }
        //幣別符號symbol
    }
}
