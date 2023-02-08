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
        /// The product's category
        /// </summary>
        public  Category ProductCategory { get; set; }

        /// <summary>
        /// The product's id
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// the product's price
        /// </summary>
        public float ProductPrice { get; set; }
    }
}