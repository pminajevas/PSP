using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoS.Core.Entities
{
    public class Business
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid? Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string BusinessName { get; set; }

        [MaxLength(500)]
        public string Location { get; set; }

        [Required]
        [Range(0, 1440)]
        public int WorkingHoursStart {  get; set; }

        [Required]
        [Range(0, 1440)]
        public int WorkingHoursEnd { get; set; }

    }
}
