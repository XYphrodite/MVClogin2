using System.ComponentModel.DataAnnotations;

namespace MVClogin2.ViewModel
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
