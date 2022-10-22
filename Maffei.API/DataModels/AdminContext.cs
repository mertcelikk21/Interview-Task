using Microsoft.EntityFrameworkCore;

namespace Maffei.API.DataModels
{
    public class AdminContext:DbContext
    {
        public AdminContext(DbContextOptions<AdminContext> options) : base(options)
        {

        }
        public DbSet<Abone> Abone { get; set; }
        public DbSet<IndexCalculation> IndexCalculation { get; set; }
        public DbSet<Kdv> Kdv { get; set; }
        public DbSet<CurrencyUnit> CurrencyUnit { get; set; }
        public DbSet<CalculationType> CalculationType { get; set; }
    }
}
