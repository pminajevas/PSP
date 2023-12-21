using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoS.Data
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

    }
}
