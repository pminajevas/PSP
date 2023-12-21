using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoS.Data
{

    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid? Id { get; set; }

        [Required]
        public Guid? BusinessId { get; set; }


        public Guid? DiscountId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ItemName { get; set; }

        [MaxLength(500)]
        public string ItemDescription { get; set; }

        [Required]
        public double? Price { get; set; }
    }
}
