using System;
namespace focusApp.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}

