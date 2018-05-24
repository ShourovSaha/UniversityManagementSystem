using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ABCUniversity.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABCUniversity.Models
{
    public class CourseInstructor
    {
        public int CourseInstructorID { get; set; }
        public int CourseID { get; set; }
        public int InstructorID { get; set; }

        [Display(Name = "Course Assign Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CourseAssignDate { get; set; }


        public virtual Course Courses { get; set; }
        public virtual Instructor Instructors { get; set; }
    }
}