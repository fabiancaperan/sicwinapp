using core.Entities.ConvertData;
using Microsoft.EntityFrameworkCore;

namespace core.Repository
{
    public class CacheContext : DbContext
    {

        public DbSet<SapModel> Sap { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseInMemoryDatabase("sic");
    }
}
