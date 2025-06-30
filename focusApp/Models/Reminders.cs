using System;
namespace focusApp.Models
{
    public class Reminder
    {
        public int ReminderId { get; set; }
        public int TaskId { get; set; }
        public DateTime RemindAt { get; set; }
        public bool IsSent { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}

