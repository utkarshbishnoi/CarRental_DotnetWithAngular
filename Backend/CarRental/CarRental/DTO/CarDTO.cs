using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.DTO
{
    public class CarDTO
    {
        [Required]
        public string Image { get; set; }
        [Required]
        public string Maker { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int AvailableQuantity { get; set; } = 1;
        [Required]
        public decimal RentalPrice { get; set; }
    }
}
