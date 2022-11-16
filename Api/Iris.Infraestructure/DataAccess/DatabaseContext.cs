using ApiTemplate.Domain.Entities;
using Iris.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.Infraestructure.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Student> Students { get; set; }        
               
    }
}
