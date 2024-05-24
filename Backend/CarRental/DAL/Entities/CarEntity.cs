using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
    public class CarEntity
    {
       
        public int Id { get; set; }
        [Required]
        public string Maker { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Image { get; set; }

        [Required]
        public decimal RentalPrice { get; set; }
        [Required]
        public int TotalRented { get; set; } = 0;
        [Required]
        public int AvailableQuantity { get; set; }

    }
}
