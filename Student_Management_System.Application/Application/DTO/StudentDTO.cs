namespace Student_Management_System.Application.DTO
{
    public class StudentDTO
    {

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public int Semester { get; set; } = 1;
        public float CGPA { get; set; } = 0.0F;
    }
}
