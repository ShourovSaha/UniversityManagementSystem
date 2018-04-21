using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ABCUniversity.Models
{
    public class OfficeAssignment
    {
        [Key]
        [ForeignKey("Instructor")]
        public int InstructorID { get; set; }
        [Display(Name = "Office Location"), StringLength(20, MinimumLength = 3)]
        public string Location { get; set; }
        public virtual Instructor Instructor { get; set; }
    }
}