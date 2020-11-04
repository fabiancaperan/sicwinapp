﻿using core.Entities.MasterData;
using Microsoft.EntityFrameworkCore;

namespace core.Repository
{
    public class dbContext : DbContext
    {
        public DbSet<EntidadesModel> EntidadesModel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=sic.db");

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<SapModel>()
        //        .HasKey(c => new { c.Cod_Trans, c.Cod_Resp, c.FechaCompra,c.Nit, c.HoraTran, c.Num_Autoriza, c.Cod_RTL, c.Num_Secuen,c.Valor });
        //}
    }

}
