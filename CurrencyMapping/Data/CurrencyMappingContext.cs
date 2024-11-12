using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CurrencyMapping.Models;

namespace CurrencyMapping.Data
{
    public class CurrencyMappingContext : DbContext
    {
        public CurrencyMappingContext (DbContextOptions<CurrencyMappingContext> options)
            : base(options)
        {
        }

        public DbSet<CurrencyMapping.Models.Currency> Currency { get; set; } = default!;
    }
}
