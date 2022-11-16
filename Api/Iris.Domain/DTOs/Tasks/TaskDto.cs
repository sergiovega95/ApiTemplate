using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Iris.Domain.DTOs.Tasks
{
    public class TaskDto
    {
        public int TaskId { get; set; }
        
        public string TaskDescription { get; set; }
    }
}
