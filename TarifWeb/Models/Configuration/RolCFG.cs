using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TarifWeb.Models.Configuration
{
    public class RolCFG : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            Rol rol = (new Rol {Id = "1", Name = "Admin", NormalizedName = "ADMIN" });
            builder.HasData(rol);
            Rol rol1 = (new Rol { Id = "2", Name = "Kullanici", NormalizedName = "KULLANICI" });
            builder.HasData(rol1);
        }
    }
}
