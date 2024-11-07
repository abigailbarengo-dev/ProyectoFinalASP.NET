﻿using Microsoft.EntityFrameworkCore;
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

            modelBuilder.Entity<CitaMascota>().HasKey(x => new { x.CitaId, x.MascotaId });
            modelBuilder.Entity<CitaVeterinario>().HasKey(x => new { x.CitaId, x.VeterinarioId });
        }

        // DbSet: creamos las tablas en la base de DATOS
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Mascota> Mascotas { get; set; }
        public DbSet<Veterinario> Veterinario { get; set; }
        public DbSet<Cita> Cita { get; set; }


        public DbSet<CitaMascota> citaMascotas { get; set; }
        public DbSet<CitaVeterinario> citaVeterinarios {  get; set; }



    }

}

