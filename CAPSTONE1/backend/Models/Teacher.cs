namespace AttendanceSystemAPI.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string TeacherNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}