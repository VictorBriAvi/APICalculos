using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using APICalculos.Domain.Entidades;

namespace APICalculos.Infrastructure.Data.Configurations
{
    public class UsuarioRolConfig : IEntityTypeConfiguration<UsuarioRol>
    {
        public void Configure(EntityTypeBuilder<UsuarioRol> builder)
        {
            builder.HasKey(ur => new { ur.UsuarioId, ur.RolId });
            builder.HasOne(ur => ur.Usuario)
                .WithMany(u => u.UsuarioRoles)
                .HasForeignKey(ur => ur.UsuarioId);

            builder.HasOne(ur => ur.Rol)
                .WithMany(r => r.RolesUsuario)
                .HasForeignKey(ur => ur.RolId);

        }
    }
}


