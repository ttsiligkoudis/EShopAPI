using Enums;
using System.ComponentModel.DataAnnotations;

namespace DataModels
{
    public class Product
    {
        public Product()
        {
            ProductRates = new HashSet<ProductRates>();
        }

        public int Id { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Category is Required")]
        public Category Category { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int? Quantity { get; set; }
        public string? Image { get; set; }
        //public byte[] Image { get; set; }
        public virtual ICollection<ProductRates> ProductRates { get; set; }
    }
}