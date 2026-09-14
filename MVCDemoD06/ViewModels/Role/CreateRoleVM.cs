using System.ComponentModel.DataAnnotations;

namespace MVCDemoD06.ViewModels.Role
{
    public class CreateRoleVM
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string RoleName { get; set; } = string.Empty;
    }
}