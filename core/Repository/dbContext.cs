using core.Entities.MasterData;
using core.Entities.UserData;
using Microsoft.EntityFrameworkCore;

namespace core.Repository
{
    public class dbContext : DbContext
    {
        public DbSet<EntidadesModel> EntidadesModel { get; set; }

        public DbSet<FalabellaModel> FalabellaModel { get; set; }

        public DbSet<CnbsModel> CnbsModel { get; set; }

        public DbSet<RedprivadasModel> RedprivadasModel { get; set; }

        public DbSet<ConveniosModel> ConveniosModel { get; set; }

        public DbSet<BinesespModel> BinesespModel { get; set; }

        public DbSet<FestivoModel> FestivoModel { get; set; }

        public DbSet<UserModel> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=sic.db");

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<SapModel>()
        //        .HasKey(c => new { c.Cod_Trans, c.Cod_Resp, c.FechaCompra,c.Nit, c.HoraTran, c.Num_Autoriza, c.Cod_RTL, c.Num_Secuen,c.Valor });
        //}
    }

}
