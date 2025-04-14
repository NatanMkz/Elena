using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Elena.Infrastructure.Data;

namespace Elena.Infrastructure
{
    public class ElenaContextFactory : IDesignTimeDbContextFactory<ElenaContext>
    {
        public ElenaContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ElenaContext>();           
            var connectionString = "Server=localhost;Database=ElenaDb;User=root;Password=password;";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new ElenaContext(optionsBuilder.Options);
        }
    }
}
