using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace firmness.Infrastructure.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // Usa tu cadena de conexión real
            optionsBuilder.UseNpgsql("Host=metro.proxy.rlwy.net;Port=51283;Database=railway;Username=postgres;Password=GUZsDllOCXNWAfhxPxtkluccEopNxzkQ");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}