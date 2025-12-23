using Microsoft.EntityFrameworkCore;
using UserApp.Models;
namespace UserApp.Models
{
    public class AppDbContext  : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=sqlserver;Initial Catalog=UERSDB;User Id=sa;Password=Password123;TrustServerCertificate=True;");
        }

        //public db(DbContextOptions<db> options):base(options) { }

    }

}


