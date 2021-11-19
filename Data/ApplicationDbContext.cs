using Microsoft.EntityFrameworkCore;
using PracticoLaboratorio4.Models;

namespace PracticoLaboratorio4.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {}

        public DbSet<Genero> Generos { get; set; }
        public DbSet<Director> Directores { get; set; }
    }
}