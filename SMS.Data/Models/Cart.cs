﻿namespace SMS.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Cart
    {
        [Key]
        [StringLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        [Required]
        public User User { get; set; }

        public ICollection<Product> Products { get; set; }

        public Cart()
        {
            Products = new List<Product>();
        }
    }
}
