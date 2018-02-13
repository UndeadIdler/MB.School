using System;
using MB.School.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace MB.School.EntityFramework
{
    public class SchoolDbContext:DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Student>().ToTable("Student");
            builder.Entity<Course>().ToTable("Course").Property(a=>a.CourseId).ValueGeneratedNever();
            builder.Entity<Enrollment>().ToTable("Enrollment").HasOne(a=>a.Course).WithMany(a=>a.Enrollments);
            builder.Entity<Enrollment>().HasOne(a => a.Student).WithMany(a => a.Enrollments);

        }

        

        public void test()
        {
            
        }
    }
}