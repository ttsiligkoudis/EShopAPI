using System.ComponentModel.DataAnnotations;

namespace DataModels.Dtos
{
    /// <summary>
    /// An instance of a customer
    /// </summary>
    public class CustomerDto
    {
        /// <summary>
        /// The id of the customer
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the customer
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The city of the customer
        /// </summary>
        //[Required]
        public string City { get; set; }

        /// <summary>
        /// The address of the customer
        /// </summary>
        //[Required]
        public string Address { get; set; }

        /// <summary>
        /// The email address of the customer
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// The registration date of the customer
        /// </summary>
        public DateTime RegDate { get; set; }

        /// <summary>
        /// The user's id that the customer may have
        /// </summary>
        public int? UserId { get; set; }
    }
}
