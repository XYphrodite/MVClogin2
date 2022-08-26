using System;
using System.ComponentModel.DataAnnotations;

namespace MVClogin2.Models
{
    public class CalibrationModel
    {
        [Key]
        public int id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string dateTime { get; set; }
    }
}
