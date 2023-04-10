
namespace DataModels.Dtos
{
    /// <summary>
    /// An instance of an order
    /// </summary>
    public class OrderDto
    {
        /// <summary>
        /// The id of the order
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The customer's name of the order
        /// </summary>
        public string? CustomerName { get; set; }

        /// <summary>
        /// The customer instance it self
        /// </summary>
        public CustomerDto? Customer { get; set; }

        /// <summary>
        /// The customer's id that the order is connected to 
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// The final price of the order
        /// </summary>
        public decimal FinalPrice { get; set; }

        /// <summary>
        /// The date that the order was placed
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// The state of the order
        /// </summary>
        public bool Completed { get; set; }
    }
}