using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDbContext: IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }

        public DbSet<Stock> Stocks { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<Portfolios> Portalofios { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Portfolios>().HasKey(p => new {p.AppUserId, p.StockId});

            builder.Entity<Portfolios>()
                .HasOne(p => p.AppUser)
                .WithMany(p => p.Portalofios)
                .HasForeignKey(p => p.AppUserId);

            builder.Entity<Portfolios>()
                .HasOne(p => p.Stock)
                .WithMany(p => p.Portalofios)
                .HasForeignKey(p => p.StockId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole {Name = "Admin", NormalizedName = "ADMIN"},
                new IdentityRole {Name = "User", NormalizedName = "USER"}
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}