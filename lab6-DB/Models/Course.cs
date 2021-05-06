using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace lab6_DB.Models
{
    public class Course
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CrsId { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 2)]
        public String CrsName { get; set; }
        public virtual List<CourseDepartment> Departments { get; set; }


    }
}