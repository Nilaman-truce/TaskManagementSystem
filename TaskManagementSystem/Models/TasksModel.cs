using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Models
{
    public class TasksModel
    {
        public int Id { get; set; }
      
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public int StatusId { get; set; }
        public string? StatusTitle { get; set; }

        public int? PriorityId { get; set; }
        public string? PriorityTitle { get; set; }
    }
}
