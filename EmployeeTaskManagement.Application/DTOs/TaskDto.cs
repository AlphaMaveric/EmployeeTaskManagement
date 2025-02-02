﻿using System.ComponentModel.DataAnnotations;

namespace EmployeeTaskManagement.Application.DTOs
{
    public class TaskDto
    {
        [Required]
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public string Notes { get; set; }

        [Required]
        public int UserId { get; set; }
        public ICollection<DocumentDto> Documents { get; set; }
    }
}
