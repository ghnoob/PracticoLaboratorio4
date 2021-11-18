using Microsoft.EntityFrameworkCore;

namespace PracticoLaboratorio4.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {}
    }
}