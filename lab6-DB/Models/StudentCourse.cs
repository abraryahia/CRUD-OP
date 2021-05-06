using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace lab6_DB.Models
{
    public class StudentCourse
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Student")]

        public int Id { get; set; }

        public virtual Student Student { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Course")]

        public int CrsId { get; set; }

        public virtual Course Course { get; set; }
        public int StuDegree { get; set; }

    }
}