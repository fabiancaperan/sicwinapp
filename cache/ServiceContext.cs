using core.Entities.ConvertData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace cache
{
    public class ServiceContext : DbContext
    {
        public DbSet<SapModel> sicModels { get; set; }
        //public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseInMemoryDatabase("sic");
    }
}
