﻿using DataModels.Dtos;
using System.ComponentModel.DataAnnotations;

namespace DataModels
{
    public class Customer
    {
        public Customer()
        {
            ProductRates = new HashSet<ProductRates>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime RegDate { get; set; }
        public User User { get; set; }
        public int? UserId { get; set; }

        public virtual ICollection<ProductRates> ProductRates { get; set; }
    }
}