using Abstraction;
using CloudinaryDotNet;
using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistance.Data;
using Persistance.Repositories;
using Services;
using WebApplication2.Claims;
using WebApplication2.Hubs;
using IdentityDbContext = Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext;

namespace WebApplication2
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
               

            builder.Services.AddAutoMapper(typeof(AssemblyReferences).Assembly);
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseLazyLoadingProxies().UseNpgsql(builder.Configuration.GetConnectionString("postgresdb"));
            });

            builder.Services.AddDbContext<AppIdentityDbContext>(Options =>
            {
                var ConnectionString = builder.Configuration.GetConnectionString("IdentityDb");

                Options.UseNpgsql(ConnectionString);
            });


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>((Option) =>
            {
                Option.User.RequireUniqueEmail = true;
                Option.Password.RequireDigit = true;
                Option.Password.RequiredLength = 8;
                Option.Password.RequiredUniqueChars = 1;
                

                Option.Lockout.AllowedForNewUsers = true;
                Option.Lockout.MaxFailedAccessAttempts = 20;
                Option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(5);

            }).AddRoles<IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";

                options.ExpireTimeSpan = TimeSpan.FromDays(2);
            });

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            builder.Services.AddSignalR();
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IDbinializer, Dbinializer>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
           
            builder.Services.AddScoped<IServiceManager, ServicesManager>();
           
           
            builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, CustomClaimsPrincipalFactory>();

            builder.Services.AddSingleton(x =>
            {
                var account = new Account(
                    "dmmoghczd",
                    "649123847344231",
                    "NE7L2Ox0W6pMBHvSjCOAibZW9AI");

                var cloudinary = new Cloudinary(account);

                cloudinary.Api.Timeout = 300000; 
                return cloudinary;
            });



            var app = builder.Build();

            await InailzeDbAsync(app); // Fixed: Call the method as static

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            
            app.UseAuthorization();

            app.MapHub<ChatHub>("/chathup");


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}");

            app.Run();
        }

        public static async Task InailzeDbAsync(WebApplication app) // Fixed: Make the method static
        {
            using var scope = app.Services.CreateScope();
            var dbInializer = scope.ServiceProvider.GetRequiredService<IDbinializer>();
            await dbInializer.InitializeAsync();
        }
    }
}
