using Microsoft.EntityFrameworkCore;
using StudentAppMigration.Models;

namespace StudentAppMigration.Data
{
    public class StudentAppDbContext : DbContext
    {
        public StudentAppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        //We want to add a DbSet for the Models in this application (in this case, it's just the Student)
        public DbSet<StudentModel> Students { get; set; }
    }
}
