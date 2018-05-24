using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ABCUniversity.Models;

namespace ABCUniversity.DAL
{
    public class VarsityContext : DbContext
    {
        public VarsityContext() : base("VarsityDBContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        //public DbSet<Person> Persons { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseInstructor> CourseInstructors { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        //public DbSet<CourseInstructor> CourseInstructors { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Course>()
            //    .HasMany(c => c.Instructors)
            //    .WithMany(i => i.Courses)
            //    .Map(t => t.MapLeftKey("CourseID")
            //    .MapRightKey("InstructorID")
            //    .ToTable("CourseInstructor"));
            modelBuilder.Entity<Department>().MapToStoredProcedures();
        }
    }
}