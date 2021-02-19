using core.Entities.ConvertData;
using Microsoft.EntityFrameworkCore;

namespace core.Repository
{
    public class CacheContext : DbContext
    {

        public DbSet<SapModel> Sap { get; set; }
        public DbSet<DateCompModel> DateComp { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseInMemoryDatabase("sic");
        //private string stringConnect = "Data Source=DESKTOP-7OU0QJ0\\MSSQLSERVER01;Initial Catalog=SIC;Integrated Security=True";
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlServer(stringConnect);
        //}

        //=> options.UseSqlite("Data Source=sic1.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SapModel>()
                .HasKey(c => new { c.Id });
            //modelBuilder.Entity<SapModel>()
            //    .HasKey(c => new { c.Nit, c.HoraTran, c.Tipo_Mensaje, c.Num_Secuen, c.Valor, c.Num_Autoriza, c.RefUniversal });
        }
    }
}
