using AspDotNetCoreMVCProject.Models;
using Microsoft.EntityFrameworkCore;

namespace AspDotNetCoreMVCProject.Data
{
    public class ApplicationDbContext : DbContext // original
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data creating by using HasData method
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, Name = "Abdur Rahim", Age = 20, Gender = "Male", Mobile = "01917890122", Email = "rahim@gmail.com" },
                new Student { Id = 2, Name = "Alamin Mia", Age = 30, Gender = "Male", Mobile = "01833838383", Email = "alamin@gmail.com" },
                new Student { Id = 3, Name = "Fatema Akter", Age = 22, Gender = "Female", Mobile = "01632828383", Email = "fatima@gmail.com" }
            );
        }
    }
}
