using System.ComponentModel.DataAnnotations;

namespace Blazor.SIMONStore.Client.Models
{
    public class UserRole
    {
        [Key]
        [Required]
        [MaxLength(450)]
        public string UserId { get; set; }

        [Key]
        [Required]
        [MaxLength(450)]
        public string RoleId { get; set; }
    }
}
