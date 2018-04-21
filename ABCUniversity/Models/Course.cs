using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ABCUniversity.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Course ID")]
        public string CourseID { get; set; }

        [Display(Name = "Course Title"), StringLength(20, MinimumLength = 2) ]
        public string Title { get; set; }

        [Range(1, 6)]
        public int Credits { get; set; }
        public int DepartmentID { get; set; }

        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual Department Department { get; set; }


    }
}