using Enums;

namespace DataModels.Dtos
{
    /// <summary>
    /// The products that an order is connected to
    /// </summary>
    public class OrderProductsDto
    {
        /// <summary>
        /// The id of the orderProduct instance
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The id of the order
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// The product's name
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// The product's id
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// The product instance it self
        /// </summary>
        public ProductDto? Product { get; set; }

        /// <summary>
        /// the product's price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// the product's quantity
        /// </summary>
        public int? Quantity { get; set; }
    }
}