using System.Globalization;

namespace Student_Management_System.Domain.Entities
{
    public abstract class User
    {
        public int UserId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; }

        public string Phone { get; set; } = string.Empty;


    }
}
