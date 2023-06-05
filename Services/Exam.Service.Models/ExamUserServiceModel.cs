using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Service.Models
{
    public class ExamUserServiceModel : BaseServiceModel
    {
        public string FirstName { get; set; }

        public string MidleName { get; set; }

        public string LastName { get; set; }

        public string EGN { get; set; }
    }
}
