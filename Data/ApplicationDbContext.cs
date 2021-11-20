using Microsoft.EntityFrameworkCore;
using PracticoLaboratorio4.Models;

namespace PracticoLaboratorio4.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genero>()
                .Property(g => g.Descripcion)
                .HasColumnType("TEXT COLLATE NOCASE");

            modelBuilder.Entity<Director>()
                .Property(d => d.Nombre)
                .HasColumnType("TEXT COLLATE NOCASE");

            modelBuilder.Entity<Pelicula>()
                .Property(p => p.Titulo)
                .HasColumnType("TEXT COLLATE NOCASE");

            modelBuilder.Entity<Genero>()
                .HasIndex(g => g.Descripcion)
                .IsUnique();
        }

        public DbSet<Genero> Generos { get; set; }
        public DbSet<Director> Directores { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
    }
}
