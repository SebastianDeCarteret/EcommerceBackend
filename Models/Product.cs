﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EcommerceBackend.Models
{
    public class Product
    {
        public int Id { get; init; }

        [Required]
        public string Name { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public string Colour { get; set; }

        [Required]
        public string Description { get; set; }

        public bool? IsInStock { get; set; } = true;

        [Required]
        public string ImageUrl { get; set; }
        public List<Review>? Reviews { get; set; }
        public Category? Category { get; set; }

        public List<Order>? Orders { get; set; } = new();

        public List<Basket>? Baskets { get; set; } = new();
    }
}