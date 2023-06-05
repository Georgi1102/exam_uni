using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.DataModels
{
    public class Car : BaseEntity
    {

        public string Mark { get; set; }

        public string Model { get; set; }

        public string YearOfProduction { get; set; }

        public int Seats { get; set; }

        public string Descritption { get; set; }

        public decimal Price { get; set; }

        public string PictureURL { get; set; }
    }
}
