using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Student_Management_System.Domain.Entities;
using Student_Management_System.Infrastructure.Settings;
using System.Reflection.Emit;

namespace Student_Management_System.Infrastructure
{
    public class StudentDbContext : IdentityDbContext<IdentityUser>
    {
        public string connection {  get; set; }

        public StudentDbContext(DbContextOptions<StudentDbContext> opt, IOptions<DbConnectionSettings> options) : base(opt)
        {
            connection = options.Value.DefaultString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connection);
        }

        private void SeedRole(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                   new IdentityRole() { Id = "A1", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN" },
                   new IdentityRole() { Id = "A2", Name = "Student", ConcurrencyStamp = "2", NormalizedName = "STUDENT" },
                   new IdentityRole() { Id = "A3", Name = "Teacher", ConcurrencyStamp = "3", NormalizedName = "TEACHER" }
                );
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            SeedRole(builder);

            builder.Entity<Student>().ToTable("Students");
            builder.Entity<Teacher>().ToTable("Teachers");


            builder.Entity<Student>(entity =>
            {
                entity.HasKey(x => x.UserId);

                entity.HasAlternateKey(x => x.StudentId);

                entity.Property(x => x.UserId)
                .IsRequired();

            });

            builder.Entity<Teacher>(entity =>
            {
                entity.HasKey(x => x.UserId);

                entity.HasAlternateKey(x => x.TeacherId);

                entity.Property(x => x.UserId)
                .IsRequired();

            });

            builder.Entity<CourseGrade>(entity =>
            {
                entity.HasKey(entity => new { entity.StudentId,entity.CourseId});
            });

            builder.Entity<CourseGrade>(entity =>
            {
                entity.HasOne(x => x.Students)
                .WithMany(x => x.courses)
                .HasForeignKey(x => x.StudentId)
                .HasPrincipalKey(x => x.StudentId);
            });

            builder.Entity<CourseGrade>(entity =>
            {
                entity.HasOne(x => x.Courses)
                .WithMany(x => x.students)
                .HasForeignKey(x => x.CourseId)
                .HasPrincipalKey(x => x.CourseId);
            });

            builder.Entity<Course>(entity =>
            {
                entity.HasKey(x => x.CourseId);
                entity.HasAlternateKey(x => x.CourseId);
                entity.HasOne(x => x.Teacher)
                .WithOne(x => x.CoursesTaught)
                .HasForeignKey<Course>(x => x.TeacherId)
                .IsRequired(false)
                .HasPrincipalKey<Teacher>(x => x.TeacherId)
                .OnDelete(DeleteBehavior.SetNull);
            });
        }

       // private DbSet<User> users {  get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }

        public DbSet<CourseGrade> CourseGrades { get; set; }
    }
}
