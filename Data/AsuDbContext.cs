using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ASUTask.Models;


namespace ASUTask.Data
{
    public  class AsuDbContext : DbContext
    {
        public AsuDbContext(DbContextOptions<AsuDbContext> options)
      : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Provider>()
               .HasMany(c => c.Orders)
               .WithOne(o => o.Provider);

            modelBuilder.Entity<Order>()
               .HasMany(c => c.OrderItems)
               .WithOne(o => o.Order);
        }

        public DbSet<Provider> Providers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
