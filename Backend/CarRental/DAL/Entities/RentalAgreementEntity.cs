using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
    public class RentalAgreementEntity
    {
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }

        public virtual CarEntity Car { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public decimal TotalCost { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public virtual User User { get; set; }


        [Required]
        public string RentalStatus { get; set; } = "pending";

        [Required]
        public bool RentalAcceptance { get; set; } = false;

        [Required]
        public bool ReqForReturn { get; set; } = false;
    }
}
