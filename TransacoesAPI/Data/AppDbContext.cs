using Microsoft.EntityFrameworkCore;
using TransacoesAPI.Mapping;
using TransacoesAPI.Models.Entity;

namespace TransacoesAPI.Data
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite(connectionString: "DataSource=app.db;Cache=Shared");

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UsuarioMap());
            builder.ApplyConfiguration(new TransacaoMap());
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }
    }
}
