using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRental.DTO
{
    public class UpdateAgreementDTO
    {
        public int CarId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string RentalStatus { get; set; }
        public bool RentalAcceptance { get; set; }
        public bool ReqForReturn { get; set; }
    }
}
