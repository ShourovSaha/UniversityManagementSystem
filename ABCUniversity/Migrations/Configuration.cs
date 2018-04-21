namespace ABCUniversity.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using ABCUniversity.Models;
    using ABCUniversity.DAL;


    internal sealed class Configuration : DbMigrationsConfiguration<ABCUniversity.DAL.VarsityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        //private VarsityContext db = new VarsityContext();

        protected override void Seed(ABCUniversity.DAL.VarsityContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            List<Student> students = new List<Student>()
            {
                new Student {FirstMidName = "Shahid", LastName = "Rana", EnrollmentDate = DateTime.Parse("2014-09-01") },
                new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
                new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
                new Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}
            };

            students.ForEach(s => context.Students.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            List<Course> courses = new List<Course>()  
            {
                new Course {CourseID = 1002, Title = "Calculus", Credits = 3 },
                new Course{CourseID=1050,Title="Chemistry",Credits=3,},
                new Course{CourseID=4022,Title="Microeconomics",Credits=3,},
                new Course{CourseID=4041,Title="Macroeconomics",Credits=3,},
                new Course{CourseID=1045,Title="Calculus",Credits=4,},
                new Course{CourseID=3141,Title="Trigonometry",Credits=4,},
                new Course{CourseID=2021,Title="Composition",Credits=3,},
                new Course{CourseID=2042,Title="Literature",Credits=4,}
            };
            courses.ForEach(c => context.Courses.AddOrUpdate(p => p.CourseID, c));
            context.SaveChanges();

            List<Enrollment> enrollments = new List<Enrollment>()
            {
                new Enrollment { StudentID = 1, CourseID = 2021, Grade = Grade.B },
                new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A},
                new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
                new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
                new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
                new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
                new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},
                new Enrollment{StudentID=3,CourseID=1050},
                new Enrollment{StudentID=4,CourseID=1050,},
                new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F},
                new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},
                new Enrollment{StudentID=6,CourseID=1045},
                new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A},
                new Enrollment {StudentID = students.Single(s => s.LastName == "Arturo").ID,
                                CourseID = courses.Single(c => c.Title == "Calculus").CourseID,
                                Grade = Grade.C}
            };
            //enrollments.ForEach(e => context.Enrollments.AddOrUpdate(e));
            

            foreach (Enrollment data in enrollments)
            {
                var studentEnrollmentsInDatabase = context.Enrollments.Where(s => s.StudentID == data.StudentID
                                                                && s.CourseID == data.CourseID).SingleOrDefault();
                if (studentEnrollmentsInDatabase == null)
                {
                    context.Enrollments.Add(data);
                }
            }
            context.SaveChanges();
        }
    }
}
