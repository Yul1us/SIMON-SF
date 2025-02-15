using System.ComponentModel.DataAnnotations;

namespace Blazor.SIMONStore.Client.Models
{
    public class Roles
    {
        [Key]
        [Required]
        [MaxLength(450)]
        public string Id { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string NormalizedName { get; set; }

        [Required]
        public string ConcurrencyStamp { get; set; }
    }
}
