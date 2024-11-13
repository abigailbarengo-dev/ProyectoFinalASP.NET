using Microsoft.EntityFrameworkCore;
using ProyectoFinalLab.Data;
using Microsoft.AspNetCore.Identity;
using System;

namespace ProyectoFinalLab.Program
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();



            // "DefaultConnection" esta en el json

            //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


            builder.Services.AddDbContext<ApplicationContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();


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

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapRazorPages();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new[] { "Admin", "Jefe", "Trabajadores" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role)) await roleManager.CreateAsync(new IdentityRole(role));
                }

            }

            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                string email = "pepe@gmail.com";
                string pass = "Prueba123?";
                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new IdentityUser();
                    user.UserName = email;
                    user.Email = email;                         // CUENTA ADMIN


                    await userManager.CreateAsync(user, pass);
                    await userManager.AddToRoleAsync(user, "Admin");
                }

            }

            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                string email = "trabajador@gmail.com";
                string pass = "Prueba123?";
                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new IdentityUser();
                    user.UserName = email;
                    user.Email = email;                     // CUENTA COLEGAS DE LA CLINICA / SECRETARIOS

                    await userManager.CreateAsync(user, pass);
                    await userManager.AddToRoleAsync(user, "Trabajadores");
                }
            }

            app.Run();
        }
    }
}
