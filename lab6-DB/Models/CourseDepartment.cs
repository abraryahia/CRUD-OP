using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab6_DB.Models
{
    public class CourseDepartment
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Department")]

        public int DeptId { get; set; }

        public virtual Department Department { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Course")]

        public int CrsId { get; set; }

        public virtual Course Course { get; set; }
    }
}