using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.DataModels
{
    public class RentCar : BaseEntity
    {
        
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string CarModel { get; set; }

        public string CarId { get; set; }

        public Car Car { get; set; }

        public decimal Price { get; set; }

        public string StatusId { get; set; }

        public Status Status { get; set; }

    }
}
