using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.DTO
{
    public class UpdateCarDTO
    {
        public string Image { get; set; }
        public string Maker { get; set; }
        public string Model { get; set; }
        public int AvailableQuantity { get; set; } = 1;
        public decimal RentalPrice { get; set; }
    }
}
