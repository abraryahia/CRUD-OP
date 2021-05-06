using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace lab6_DB.Models
{
    public class Student
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string FName { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string LName { get; set; }
        [Range(17, 30)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Age { get; set; }
        [Required]
        [RegularExpression(@"[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)")]
        //[RegularExpression(@"[a-zA-Z0-9]+@[a-zA-z]+.[a-zA-Z]{2-4}")]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string UserName { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [NotMapped]
        //[Compare("Password")]
        public string CPassword { get; set; }
        public string photo { get; set; }
        public int DeptId { get; set; }
        [ForeignKey("DeptId")]

        public virtual Department department { get; set; }
        public virtual List<StudentCourse> Courses { get; set; }

    }
}