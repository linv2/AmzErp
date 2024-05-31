using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using AmzErp.DataAccess.Entity;

namespace AmzErp.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class AmzErpDbContext : DbContext
    {
        public AmzErpDbContext(DbContextOptions<AmzErpDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
        }

        public DbSet<User> User { get; set; }

        public DbSet<AmzShop> AmzShop { get; set; }
        public DbSet<AmzMarketplace> AmzMarketplace { get; set; }

        public DbSet<ErpCategory> ErpCategory { get; set; }
    }
}