namespace AttendanceSystemAPI.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string StudentNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Section { get; set; } = string.Empty;
        public string Strand { get; set; } = string.Empty;
    }
}