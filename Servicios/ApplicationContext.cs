using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalLab.Models;

namespace ProyectoFinalLab.Servicios
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }


        // DbSet: creamos las tablas en la base de DATOS
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Mascota> Mascotas { get; set; }  
        public DbSet<Veterinario> Veterinario { get; set; }
        public DbSet<Cita> Cita { get; set; }

    }

}

