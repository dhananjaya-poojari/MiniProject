using Microsoft.EntityFrameworkCore;
using TinyUrl.Models;

namespace TinyUrl.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<URL> URLs { get; set; }
    }
}
