using System.ComponentModel.DataAnnotations;

namespace PoS.Application.Models.Requests
{
    public class BusinessRequest
    {
        [Required]
        [MaxLength(100)]
        public string BusinessName { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Location { get; set; } = string.Empty;

        [Required]
        [Range(0, 1440)]
        public int WorkingHoursStart { get; set; }

        [Required]
        [Range(0, 1440)]
        public int WorkingHoursEnd { get; set; }
    }
}
