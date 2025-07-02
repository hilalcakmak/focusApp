using System;
namespace focusApp.Models
{
    public class PomodoroSession
    {
        public int SessionId { get; set; }
        public int TaskId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public bool IsBreak { get; set; }
    }

}

