using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace lab6_DB.Models
{
    public class Department
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DeptId { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 2)]
        public String DeptName { get; set; }
        //public int CourseId { get; set; }
         public virtual List<Student> Students { get; set; }
        public virtual List<CourseDepartment> Course { get; set; }

    }
}