﻿using core.Entities.ConvertData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace core.Repository
{
    public class ServiceContext : DbContext
    {
        public DbSet<SapModel> sicModels { get; set; }
        //public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseInMemoryDatabase("sic");

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<SapModel>()
        //        .HasKey(c => new { c.Cod_Trans, c.Cod_Resp, c.FechaCompra,c.Nit, c.HoraTran, c.Num_Autoriza, c.Cod_RTL, c.Num_Secuen,c.Valor });
        //}
    }
}
