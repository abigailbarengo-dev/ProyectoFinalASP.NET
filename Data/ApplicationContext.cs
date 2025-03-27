using Microsoft.EntityFrameworkCore;
using ProyectoFinalLab.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ProyectoFinalLab.Data
{
    public class ApplicationContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        // DbSet: creamos las tablas en la base de DATOS
        public DbSet<Cliente> Clientes { get; set; } = default!;
        public DbSet<Mascota> Mascotas { get; set; } = default!;
        public DbSet<Veterinario> Veterinario { get; set; } = default!;
        public DbSet<Citas> Citas { get; set; } = default!;





    }

}

