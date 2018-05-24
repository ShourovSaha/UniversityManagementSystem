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
            //context.SaveChanges();

            List<Instructor> instructors = new List<Instructor>
            {
                new Instructor { FirstMidName = "Kim", LastName = "Abercrombie",
                    HireDate = DateTime.Parse("1995-03-11") },
                new Instructor { FirstMidName = "Fadi", LastName ="Fakhouri",
                    HireDate = DateTime.Parse("2002-07-06") },
                new Instructor { FirstMidName = "Roger", LastName = "Harui",
                    HireDate = DateTime.Parse("1998-07-01") },
                new Instructor { FirstMidName = "Candace", LastName ="Kapoor",
                    HireDate = DateTime.Parse("2001-01-15") },
                new Instructor { FirstMidName = "Roger", LastName ="Zheng",
                    HireDate = DateTime.Parse("2004-02-12") }
            };

            instructors.ForEach(s => context.Instructors.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var departments = new List<Department>()
            {
                new Department {/*DepartmentID = 1001,*/ DepartmentName = "Computer Science and Engineerng", Budget = 1000000, StartDate = DateTime.Parse("01-01-2000"),
                    InstructorID = instructors.Single(i => i.LastName == "Fakhouri").InstructorID},
                new Department {/*DepartmentID = 1002,*/ DepartmentName = "Mathematics", Budget = 100000, StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID = instructors.Single( i => i.LastName == "Fakhouri").InstructorID },
                new Department {/*DepartmentID = 1003,*/ DepartmentName = "Electric & Electronic Engineering", Budget = 350000, StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID = instructors.Single( i => i.LastName =="Harui").InstructorID },
                new Department {/*DepartmentID = 1004,*/ DepartmentName = "Economics", Budget = 100000,StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID = instructors.Single( i => i.LastName =="Kapoor").InstructorID }
            };

            departments.ForEach(d => context.Departments.AddOrUpdate(i=> i.DepartmentID, d));
            context.SaveChanges();

            List<Course> courses = new List<Course>()
            {
                new Course {CourseID = 101, Title = "Calculus", Credits = 3,
                DepartmentID = departments.Single(d => d.DepartmentName == "Mathematics").DepartmentID,
                //CourseInstructors = new List<Instructor>()
                },
                new Course{CourseID=102,Title="Chemistry",Credits=3,
                    DepartmentID = departments.Single(d => d.DepartmentName == "Electric & Electronic Engineering").DepartmentID,
                    //CourseInstructors = new List<Instructor>()
                },
                
                new Course {CourseID = 201, Title = "Microeconomics", Credits = 3,
                    DepartmentID = departments.Single( s => s.DepartmentName == "Economics").DepartmentID,
                    //CourseInstructors = new List<Instructor>()
                },
                
                new Course {CourseID = 203, Title = "Calculus", Credits = 4,
                    DepartmentID = departments.Single( s => s.DepartmentName == "Mathematics").DepartmentID,
                    //CourseInstructors = new List<Instructor>()
                },
                new Course {CourseID = 301, Title = "Trigonometry",Credits = 4,
                    DepartmentID = departments.Single( s => s.DepartmentName == "Mathematics").DepartmentID,
                    //CourseInstructors = new List<Instructor>()
                },
                new Course {CourseID = 302, Title = "Composition", Credits = 3,
                DepartmentID = departments.Single( s => s.DepartmentName == "English").DepartmentID,
                //CourseInstructors = new List<Instructor>()
                }
                //new Course{CourseID="MiCR201",Title="Microeconomics",Credits=3,},
                //new Course{CourseID="MACR202",Title="Macroeconomics",Credits=3,},
                //new Course{CourseID="CAL203",Title="Calculus",Credits=4,},
                //new Course{CourseID="TRIG301",Title="Trigonometry",Credits=4,},
                //new Course{CourseID="COMP302",Title="Composition",Credits=3,},
                //new Course{CourseID="LIT104",Title="Literature",Credits=4,}
            };
            courses.ForEach(c => context.Courses.AddOrUpdate(p => p.CourseID, c));
            context.SaveChanges();


            var courseInstructors =new  List< CourseInstructor >()
            {
                new CourseInstructor { InstructorID = instructors.Single(i => i.LastName == "Abercrombie").InstructorID,
                                       CourseID = courses.Single(c => c.CourseID == 101).CourseID
                },


            };

            List<Enrollment> enrollments = new List<Enrollment>()
            {
                //new Enrollment { StudentID = 1, CourseID = "CHE102", Grade = Grade.B },
                //new Enrollment{StudentID=1,CourseID="CAL101",Grade=Grade.A},
                //new Enrollment{StudentID=1,CourseID="MACR202",Grade=Grade.C},
                //new Enrollment{StudentID=1,CourseID="CAL203",Grade=Grade.B},
                //new Enrollment{StudentID=2,CourseID="TRIG301",Grade=Grade.B},
                //new Enrollment{StudentID=2,CourseID="LIT104",Grade=Grade.F},
                //new Enrollment{StudentID=2,CourseID="MiCR201",Grade=Grade.F},
                //new Enrollment{StudentID=3,CourseID="CAL203"},
                //new Enrollment{StudentID=4,CourseID="CHE102"},
                //new Enrollment{StudentID=4,CourseID="LIT104",Grade=Grade.F},
                //new Enrollment{StudentID=5,CourseID="TRIG301",Grade=Grade.C},
                //new Enrollment{StudentID=6,CourseID="CHE102"},
                //new Enrollment{StudentID=7,CourseID="CAL101",Grade=Grade.A},
                new Enrollment {StudentID = students.Single(s => s.LastName == "Arturo").StudentID,
                                CourseID = courses.Single(c => c.CourseID == 301).CourseID,
                                Grade = Grade.C},
                new Enrollment {StudentID = students.Single(s => s.LastName == "Alexander").StudentID,
                                CourseID = courses.Single(c => c.CourseID == 201 ).CourseID,
                                Grade = Grade.B },
                new Enrollment {
                                StudentID = students.Single(s => s.LastName == "Alonso").StudentID,
                                CourseID = courses.Single(c => c.CourseID == 101).CourseID,
                                Grade = Grade.B },
                new Enrollment {
                                StudentID = students.Single(s => s.LastName == "Alonso").StudentID,
                                CourseID = courses.Single(c => c.CourseID == 301 ).CourseID,
                                Grade = Grade.B },
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

            var officeAssignments = new List<OfficeAssignment>
            {
                new OfficeAssignment { InstructorID = instructors.Single( i => i.LastName == "Fakhouri").InstructorID, Location = "Smith 17" },
                new OfficeAssignment { InstructorID = instructors.Single( i => i.LastName == "Harui").InstructorID, Location = "Gowan 27" },
                new OfficeAssignment { InstructorID = instructors.Single( i => i.LastName == "Kapoor").InstructorID, Location = "Thompson 304" },
            };
            officeAssignments.ForEach(s =>
            context.OfficeAssignments.AddOrUpdate(p => p.InstructorID, s));
            context.SaveChanges();

           
        }
    }
}
