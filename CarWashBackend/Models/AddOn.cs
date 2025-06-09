using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class AddOn{
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; } = true;
        public ICollection<OrderAddOn> OrderAddOns{get;set;}
    }