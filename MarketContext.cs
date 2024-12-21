using Microsoft.EntityFrameworkCore;
using Models;
namespace marketContext
{
    public class MarketContext : DbContext
    {
        public DbSet<Ticker> Tickers => Set<Ticker>();
        public DbSet<Price> Prices => Set<Price>();
        public DbSet<TodaysCondition> todaysConditions => Set<TodaysCondition>();
        public MarketContext() => Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=MarketDB.db;");
        }
    }
}
