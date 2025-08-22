using Educational_Courses_Platform.Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Educational_Courses_Platform.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<PaidCourse> PaidCourses { get; set; }
        public DbSet<Episode> Episodes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Course Id auto-increment
            modelBuilder.Entity<Course>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            // PaidCourse Id auto-increment
            modelBuilder.Entity<PaidCourse>()
                .Property(pc => pc.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ApplicationUser>()
            .HasIndex(u => u.Email)
             .IsUnique();
        }
    }
}
