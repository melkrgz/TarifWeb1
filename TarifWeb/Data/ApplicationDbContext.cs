using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TarifWeb.Models;
using TarifWeb.Models.Configuration;

namespace TarifWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Kullanici> ApplicationUsers { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Yemek> Yemek { get; set; }
        public DbSet<Yorum> Yorum { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           
            builder.ApplyConfiguration(new KullaniciCFG());
            builder.ApplyConfiguration(new RolCFG());
            builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "1",
                UserId = "1"
            }
        );

            base.OnModelCreating(builder);
        }
    }
}
