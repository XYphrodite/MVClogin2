using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.Jwt;
using MVClogin2.Areas.Identity.Data;
using MVClogin2.Areas.Identity.Pages.Account;
using MVClogin2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Owin;
using Microsoft.Owin.Security;

using Owin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;

namespace MVClogin2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            //p
            services.AddTransient<JsonFileProductService>();
            services.AddTransient<JsonFileSqlConstService>();
            //jwt---------------------------------------

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidateLifetime = true,
            //            ValidateIssuerSigningKey = true,
            //            ValidIssuer = Configuration["Jwt:Issuer"],
            //            ValidAudience = Configuration["Jwt:Audience"],
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
            //        };
            //    });
            //services.AddAuthentication();

            //multi-authentification policy--------------------------------



            services.AddAuthentication(options =>
            {
                // these must be set other ASP.NET Core will throw exception that no
                // default authentication scheme or default challenge scheme is set.
                options.DefaultAuthenticateScheme =
                        CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme =
                        CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie();
            services.AddMvc(options => options.Filters.Add(new
                     RequireHttpsAttribute()));

            //services.AddAuthentication(o =>
            //  {
            //      o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //      o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //  })
            //  .AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidateLifetime = true,
            //            ValidateIssuerSigningKey = true,
            //            ValidIssuer = Configuration["Jwt:Issuer"],
            //            ValidAudience = Configuration["Jwt:Audience"],
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
            //        };
            //    })
            //  .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
            //  {
            //      o.ExpireTimeSpan = TimeSpan.FromMinutes(30); // optional
            //  });
            //var multiSchemePolicy = new Authentication(
            //        CookieAuthenticationDefaults.AuthenticationScheme,
            //        JwtBearerDefaults.AuthenticationScheme
            //        )
            //      .RequireAuthenticatedUser()
            //      .Build();

            //services.AddAuthentication(o => o.DefaultAuthenticateScheme = multiSchemePolicy);


            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<IdentityDbContext>();

            services.Configure<IdentityOptions>(opts =>
            {
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
                opts.User.RequireUniqueEmail = true;
            });

            services.AddAuthentication(opts =>
            {
                opts.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(//opts =>
            /*{
                opts.Events.DisableRedirectForPath(e => e.OnRedirectToLogin, "/api", StatusCodes.Status401Unauthorized);
                opts.Events.DisableRedirectForPath(e => e.OnRedirectToAccessDenied, "/api", StatusCodes.Status403Forbidden);
            }*/).AddJwtBearer(opts =>
            {
                opts.RequireHttpsMetadata = false;
                opts.SaveToken = true;
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(Configuration["jwtSecret"])),
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
                opts.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async ctx =>
                    {
                        var usrmgr = ctx.HttpContext.RequestServices.GetRequiredService<UserManager<IdentityUser>>();
                        var signinmgr = ctx.HttpContext.RequestServices.GetRequiredService<SignInManager<IdentityUser>>();
                        string username = ctx.Principal.FindFirst(ClaimTypes.Name)?.Value;
                        IdentityUser idUser = await usrmgr.FindByNameAsync(username);
                        ctx.Principal = await signinmgr.CreateUserPrincipalAsync(idUser);
                    }
                };
            });

            services.AddMvc();
            services.AddControllers();
            //services.AddMvc(config =>
            //{
            //    var policy = new AuthorizationPolicyBuilder()
            //                     .RequireAuthenticatedUser()
            //                     .Build();
            //    config.Filters.Add(new AuthorizeFilter(policy));
            //});

            services.AddScoped<IPasswordHasher<ApplicationUser>, CustomPasswordHasher>();

            //cookie-------------------------------------------------

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            //--------------------
            app.UseAuthentication();

            app.UseAuthorization();
            //--------------------
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
            //cookie
            //var cookiePolicyOptions = new CookiePolicyOptions
            //{
            //    MinimumSameSitePolicy = SameSiteMode.Strict,
            //};
            //app.UseCookiePolicy(cookiePolicyOptions);


            //    app.UseCors(x => x
            //.WithOrigins("https://localhost:3000") // путь к нашему SPA клиенту
            //.AllowCredentials()
            //.AllowAnyMethod()
            //.AllowAnyHeader());
            //    app.UseCookiePolicy(new CookiePolicyOptions
            //    {
            //        MinimumSameSitePolicy = SameSiteMode.Strict,
            //        HttpOnly = HttpOnlyPolicy.Always,
            //        Secure = CookieSecurePolicy.Always
            //    });
            //    app.Use(async (context, next) =>
            //    {
            //        var token = context.Request.Cookies[".AspNetCore.Application.Id"];
            //        if (!string.IsNullOrEmpty(token))
            //            context.Request.Headers.Add("Authorization", "Bearer " + token);

            //        await next();
            //    });
            //    app.UseAuthentication();

            //ConfigureOAuthTokenConsumption(app);
        }
    }
}
