using Abstraction;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Data;
using Persistance.Repositories;
using Services;
using System.Threading.Tasks;
//using Persistance.Data;

namespace WebApplication1
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
            }
           );

            builder.Services.AddScoped<IDbinializer, Dbinializer>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IServiceManager, ServicesManager>();

          

            var app = builder.Build();

            await InailzeDbAsync(app);

            // Configure the HTTP request pipeline.  
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        public static async Task InailzeDbAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope(); //using to automatic deleted by clr dispose

            var dbInializer = scope.ServiceProvider.GetRequiredService<IDbinializer>();
            await dbInializer.InitializeAsync();

        }
    }

}
