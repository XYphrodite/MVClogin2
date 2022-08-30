using System;

namespace MVClogin2.Models
{
    public class UserMessage
    {
        public string Username { get; set; }
        public string Message { get; set; }
        public bool IsCurrentUser { get; set; }
        public DateTime DateSent { get; set; }
    }
}
