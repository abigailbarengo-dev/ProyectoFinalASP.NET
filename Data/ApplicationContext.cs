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

            // Configuración explícita de la relación uno a muchos entre Citas y Veterinario
            modelBuilder.Entity<Citas>()
                .HasOne(c => c.veterinario)  // Una Cita tiene un solo Veterinario
                .WithMany(v => v.Citas)      // Un Veterinario puede tener muchas Citas
                .HasForeignKey(c => c.VeterinarioId);  // La clave foránea en Citas es VeterinarioId
        }

        // DbSet: creamos las tablas en la base de DATOS
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Mascota> Mascotas { get; set; }
        public DbSet<Veterinario> Veterinario { get; set; }
        public DbSet<Citas> Citas { get; set; }





    }

}

