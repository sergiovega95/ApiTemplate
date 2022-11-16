using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iris.Domain.Entities
{
    [Table("Task", Schema = "dbo")]
    public  class Task
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required(ErrorMessage = "TaskId is required")]
        [Column("TaskId")]
        public int TaskId { get; set; }

        [Required(ErrorMessage = "TaskDescription is required")]
        [MaxLength(500)]
        [Column("TaskDescription")]
        public string TaskDescription { get; set; }

        [Required(ErrorMessage = "DateCreated is required")]
        [Column("DateCreated")]
        public DateTime DateCreated { get; set; }
                
        [Column("DateModified")]
        [Required(ErrorMessage = "DateModified is required")]
        public DateTime DateModified { get; set; }
    }
}
