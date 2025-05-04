using Elena.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Elena.Infrastructure.Data
{
    public class ElenaContext : DbContext
    {
        public ElenaContext(DbContextOptions<ElenaContext> options) : base(options) { }

        public DbSet<Lutador> Lutadores { get; set; } = default!;

        public DbSet<Usuario> Usuarios { get; set; } = default!;

        public DbSet<Pessoa> Pessoas { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Pessoa)
                .WithOne()
                .HasForeignKey<Usuario>(u => u.PessoaId);
        }
    }

}
