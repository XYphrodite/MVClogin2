using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVClogin2.Areas.Identity.Data;
using MVClogin2.Sql.Data;

[assembly: HostingStartup(typeof(MVClogin2.Areas.Identity.IdentityHostingStartup))]
namespace MVClogin2.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ApplicationDbContextConnection")));
                services.AddDbContext<CustomDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("CustomDbContextConnection")));
            });
        }
    }
}