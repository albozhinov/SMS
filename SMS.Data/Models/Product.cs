namespace SMS.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        [Key]
        [StringLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(20, MinimumLength = 4)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "money")]
        [Range(0.05, 1000)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(36)]
        public string CartId { get; set; }

        public Cart Cart { get; set; }
    }
}
