using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApiTemplate.Domain.Entities
{
    [Table("Student",Schema ="dbo")]
    public class Student
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        [Column("StudentId")]
        public int StudentId { get; set; }

        [Required]
        [MaxLength(250)]
        [Column("StudentName")]
        public string StudentName { get; set;}

        [Required]
        [MaxLength(250)]
        [Column("StudentEmail")]
        public string StudentEmail { get; set;}
    }
}
