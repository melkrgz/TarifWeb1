using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TarifWeb.Models.Configuration
{
    public class KullaniciCFG:IEntityTypeConfiguration<Kullanici>
    {
        public void Configure(EntityTypeBuilder<Kullanici> builder)
        {
            Kullanici kullanici = (new Kullanici {Id = "1", Ad = "admin", Soyad = "admin", UserName = "admin@admin.com", NormalizedUserName = "ADMIN@ADMIN.COM", Email = "admin@admin.com", NormalizedEmail = "ADMIN@ADMIN.COM", EmailConfirmed = false, PhoneNumber = "5155155555" ,Rol = "Admin"});
            PasswordHasher<Kullanici> hash = new PasswordHasher<Kullanici>();
            kullanici.PasswordHash = hash.HashPassword(kullanici, "Admin123*");
            builder.HasData(kullanici);


        }
    }
}
