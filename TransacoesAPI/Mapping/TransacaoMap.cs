using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TransacoesAPI.Models.Entity;

namespace TransacoesAPI.Mapping
{
    public class TransacaoMap : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.ToTable("Transacao");
            builder.HasKey(x => x.Id);


            builder.Property(x => x.TipoDaMovimentacao)
                .IsRequired()
                .HasColumnName("TipoDaMovimentacao")
                .HasColumnType("INT");


            builder.Property(x => x.DataHoraCriacao)
                .IsRequired()
                .HasColumnName("DataHoraCriacao")
                .HasColumnType("DATETIME");

            builder.HasOne(x => x.Usuario)
                .WithMany().HasForeignKey(x => x.UsuarioId).IsRequired();

        }
    }
}
