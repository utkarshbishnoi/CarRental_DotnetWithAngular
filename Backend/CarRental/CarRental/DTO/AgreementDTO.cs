using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.DTO
{
    public class AgreementDTO
    {
        [Required]
        public int CarId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]                                                          
        public DateTime EndDate { get; set; }


        [Required]
        public int UserId { get; set; }
    }
}
