using Elena.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Elena.Infrastructure.Data
{
    public class ElenaContext : DbContext
    {
        public ElenaContext(DbContextOptions<ElenaContext> options) : base(options) { }

        public DbSet<Lutador> Lutadores { get; set; } = default!;
    }
}
