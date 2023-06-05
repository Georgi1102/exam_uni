using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Service.Models
{
    public class RentCarServiceModel : BaseServiceModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string CarModel { get; set; }

        public string CarId { get; set; }

        public CarServiceModel Car { get; set; }

        public decimal Price { get; set; }

        public string StatusId { get; set; }

        public StatusServiceModel Status { get; set; }
    }
}
