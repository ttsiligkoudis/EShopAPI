using Enums;
using System.ComponentModel.DataAnnotations;

namespace DataModels.Dtos
{
    /// <summary>
    /// An instance of a product
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// The id of the product
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the product
        /// </summary>
        [Required]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        /// <summary>
        /// The category of the product
        /// </summary>
        [Required(ErrorMessage = "Category is Required")]
        public Category Category { get; set; }

        /// <summary>
        /// The description of the product
        /// </summary>
        [Required]
        [Display(Name = "Product description")]
        public string Description { get; set; }

        /// <summary>
        /// The price of the product
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// The remaining quantity of the product
        /// </summary>
        public int? Quantity { get; set; }

        /// <summary>
        /// The image of the product
        /// </summary>
        [Display(Name = "Product image")]
        public string? Image { get; set; }
        //public byte[] Image { get; set; }

        /// <summary>
        /// The rate of the product
        /// </summary>
        public decimal? Rate { get; set; }
    }
}