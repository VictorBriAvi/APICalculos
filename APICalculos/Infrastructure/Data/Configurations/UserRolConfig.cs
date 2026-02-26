using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using APICalculos.Domain.Entidades;

namespace APICalculos.Infrastructure.Data.Configurations
{
    public class UserRolConfig : IEntityTypeConfiguration<UserRol>
    {
        public void Configure(EntityTypeBuilder<UserRol> builder)
        {
            builder.HasKey(x => new { x.UserId, x.RolId });

            builder.HasOne(x => x.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Rol)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(x => x.RolId);
        }
    }

}


