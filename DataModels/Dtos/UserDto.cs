using Enums;
using System.ComponentModel.DataAnnotations;

namespace DataModels.Dtos
{
    /// <summary>
    /// An instance of a user
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// The id of the user
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The email address of the user
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// The password of the user
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// The type of the user
        /// </summary>
        [Required]
        public UserType UserType { get; set; }

        /// <summary>
        /// The registration date of the user
        /// </summary>
        [Display(Name = "Registration Date")]
        public DateTime RegDate { get; set; }

        /// <summary>
        /// The last date the user logged in
        /// </summary>
        [Display(Name = "Last Login Date")]
        public DateTime? LoginDate { get; set; }
    }
}