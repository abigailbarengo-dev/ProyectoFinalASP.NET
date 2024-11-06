using Microsoft.EntityFrameworkCore;
using ProyectoFinalLab.Data;
using Microsoft.AspNetCore.Identity;
using ProyectoFinalLab.Areas.Identity.Data;

namespace ProyectoFinalLab.Program
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();



            // "DefaultConnection" esta en el json

            //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


            builder.Services.AddDbContext<ApplicationContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);

            });

            builder.Services.AddDefaultIdentity<ProyectoFinalLabUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ProyectoFinalLabContext>();

            var app = builder.Build(); 


            //options.UseSqlServer(connectionString);



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
