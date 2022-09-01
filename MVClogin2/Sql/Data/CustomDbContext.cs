using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MVClogin2.Models;
using System.IO;

namespace MVClogin2.Sql.Data
{
    public class CustomDbContext : DbContext
    {
        public CustomDbContext(DbContextOptions<CustomDbContext> options)
            : base(options)
        {
        }
        public DbSet<CalibrationModel> Calibrations { get; set; } = null;
        public DbSet<UserMessage> userMessages { get; set; } = null;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("CustomDbContextConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
