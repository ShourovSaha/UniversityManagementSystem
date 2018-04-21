﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABCUniversity.Models
{
    public class Student
    {
        public int ID { get; set; }
        [StringLength(10, MinimumLength = 1, ErrorMessage = "First name must be with in 10 words.")]
        public string LastName { get; set; }

        [StringLength(10, MinimumLength = 1, ErrorMessage = "First name must be with in 10 words.")]
        [Column("FirstName")]
        public string FirstMidName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return this.FirstMidName + " " + this.LastName;
            }
        }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}