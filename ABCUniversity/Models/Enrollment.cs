﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABCUniversity.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int StudentID { get; set; }
        public int CourseID { get; set; }
        public Grade? Grade { get; set; }

        public virtual Student Students { get; set; }
        public virtual Course Courses { get; set; }
    }
}