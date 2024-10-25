﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProyectoFinalLab.Servicios;

#nullable disable

namespace ProyectoFinalLab.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20241025022526_VetCita")]
    partial class VetCita
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CitaCliente", b =>
                {
                    b.Property<int>("CitasId")
                        .HasColumnType("int");

                    b.Property<int>("ClientesId")
                        .HasColumnType("int");

                    b.HasKey("CitasId", "ClientesId");

                    b.HasIndex("ClientesId");

                    b.ToTable("CitaCliente");
                });

            modelBuilder.Entity("ClienteMascota", b =>
                {
                    b.Property<int>("ClientesId")
                        .HasColumnType("int");

                    b.Property<int>("MascotasId")
                        .HasColumnType("int");

                    b.HasKey("ClientesId", "MascotasId");

                    b.HasIndex("MascotasId");

                    b.ToTable("ClienteMascota");
                });

            modelBuilder.Entity("ProyectoFinalLab.Models.Cita", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("Fecha")
                        .HasColumnType("date");

                    b.Property<TimeOnly>("Hora")
                        .HasColumnType("time");

                    b.Property<string>("Motivo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreVet")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cita");
                });

            modelBuilder.Entity("ProyectoFinalLab.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("ProyectoFinalLab.Models.Mascota", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CitaId")
                        .HasColumnType("int");

                    b.Property<string>("Especie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Raza")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CitaId");

                    b.ToTable("Mascotas");
                });

            modelBuilder.Entity("ProyectoFinalLab.Models.Veterinario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CitaId")
                        .HasColumnType("int");

                    b.Property<string>("Especialidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CitaId");

                    b.ToTable("Veterinario");
                });

            modelBuilder.Entity("CitaCliente", b =>
                {
                    b.HasOne("ProyectoFinalLab.Models.Cita", null)
                        .WithMany()
                        .HasForeignKey("CitasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectoFinalLab.Models.Cliente", null)
                        .WithMany()
                        .HasForeignKey("ClientesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClienteMascota", b =>
                {
                    b.HasOne("ProyectoFinalLab.Models.Cliente", null)
                        .WithMany()
                        .HasForeignKey("ClientesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectoFinalLab.Models.Mascota", null)
                        .WithMany()
                        .HasForeignKey("MascotasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProyectoFinalLab.Models.Mascota", b =>
                {
                    b.HasOne("ProyectoFinalLab.Models.Cita", null)
                        .WithMany("Mascotas")
                        .HasForeignKey("CitaId");
                });

            modelBuilder.Entity("ProyectoFinalLab.Models.Veterinario", b =>
                {
                    b.HasOne("ProyectoFinalLab.Models.Cita", null)
                        .WithMany("Veterinario")
                        .HasForeignKey("CitaId");
                });

            modelBuilder.Entity("ProyectoFinalLab.Models.Cita", b =>
                {
                    b.Navigation("Mascotas");

                    b.Navigation("Veterinario");
                });
#pragma warning restore 612, 618
        }
    }
}
