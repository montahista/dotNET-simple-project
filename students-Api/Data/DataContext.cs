using Microsoft.EntityFrameworkCore;
using students_Api.Entities;

namespace students_Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
    }
}
