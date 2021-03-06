// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PracticoLaboratorio4.Data;

namespace PracticoLaboratorio4.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211120022739_CaseInsensitive")]
    partial class CaseInsensitive
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("PracticoLaboratorio4.Models.Director", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Biografia")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT COLLATE NOCASE");

                    b.Property<string>("UrlFoto")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Directores");
                });

            modelBuilder.Entity("PracticoLaboratorio4.Models.Genero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT COLLATE NOCASE");

                    b.HasKey("Id");

                    b.HasIndex("Descripcion")
                        .IsUnique();

                    b.ToTable("Generos");
                });

            modelBuilder.Entity("PracticoLaboratorio4.Models.Pelicula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DirectorId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaEstreno")
                        .HasColumnType("TEXT");

                    b.Property<int>("GeneroId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Resumen")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT COLLATE NOCASE");

                    b.Property<string>("Trailer")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<string>("UrlImagenPortada")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DirectorId");

                    b.HasIndex("GeneroId");

                    b.ToTable("Peliculas");
                });

            modelBuilder.Entity("PracticoLaboratorio4.Models.Pelicula", b =>
                {
                    b.HasOne("PracticoLaboratorio4.Models.Director", "Director")
                        .WithMany("Peliculas")
                        .HasForeignKey("DirectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PracticoLaboratorio4.Models.Genero", "Genero")
                        .WithMany("Peliculas")
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Director");

                    b.Navigation("Genero");
                });

            modelBuilder.Entity("PracticoLaboratorio4.Models.Director", b =>
                {
                    b.Navigation("Peliculas");
                });

            modelBuilder.Entity("PracticoLaboratorio4.Models.Genero", b =>
                {
                    b.Navigation("Peliculas");
                });
#pragma warning restore 612, 618
        }
    }
}
