using Microsoft.AspNetCore.Identity;

namespace Exam.DataModels
{
    public class ExamUser : IdentityUser<string>
    {

        public string FirstName { get; set; }

        public string MidleName { get; set; }

        public string LastName { get; set; }

        public string EGN { get; set; }

    }
}
