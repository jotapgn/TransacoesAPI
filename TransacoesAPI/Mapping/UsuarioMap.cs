using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TransacoesAPI.Models.Entity;

namespace TransacoesAPI.Mapping
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(x => x.Id);
            

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnName("Name")
                .HasMaxLength(90)
                .HasColumnType("VARCHAR(90)");

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasMaxLength(250)
                .HasColumnType("VARCHAR(250)");

            builder.HasIndex(x => x.Email);

            builder.Property(x => x.Password)
                .IsRequired()
                .HasColumnName("Password")
                .HasMaxLength(500)
                .HasColumnType("VARCHAR(500)");

        }
    }
}
