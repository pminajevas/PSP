using System.ComponentModel.DataAnnotations;

namespace PoS.Data
{
    public class Business
    {
        [Key]
        public Guid? Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string BusinessName { get; set; }

        [MaxLength(500)]
        public string Location { get; set; }

    }
}
