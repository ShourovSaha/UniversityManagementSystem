using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ABCUniversity.Models;

namespace ABCUniversity.ViewModels
{
    public class InstructorIndexData
    {
        public IEnumerable<Instructor> Instructors { get; set; }
        public IEnumerable<CourseInstructor> CourseInstructors { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
    }
}