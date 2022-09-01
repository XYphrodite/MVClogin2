using System;
using System.ComponentModel.DataAnnotations;

namespace MVClogin2.Models
{
    public class UserMessage
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string userId { get; set; }
        public string groupId { get; set; }
        public string Message { get; set; }
        //public bool IsCurrentUser { get; set; }
        public DateTime DateSent { get; set; }
    }
}
