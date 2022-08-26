using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MVClogin2.Models;
using MVClogin2.Services;

namespace MVClogin2.Sql.Data
{
    public class CustomDbContext : DbContext
    {
        public CustomDbContext(DbContextOptions<CustomDbContext> options)
            : base(options)
        {
        }
        public DbSet<CalibrationModel> Calibrations { get; set; } = null;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!1
            optionsBuilder.UseSqlServer("Server=DESKTOP-6O5PLDP;Database=MVClogin4;user id=someuser; password=12345;Trusted_Connection=True;MultipleActiveResultSets=true");
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!1
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<CustomDbContext>(
                    eb =>
                    {
                        eb.HasNoKey();
                    })
                .Entity<CalibrationModel>(
                    eb =>
                    {
                        eb.HasNoKey();
                    });
        }
    }
}
