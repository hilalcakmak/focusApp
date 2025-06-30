using System;
namespace focusApp.Models
{
    public class FocusTask
    {
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public int? ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte Priority { get; set; }
        public int? EstimatedMins { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}

