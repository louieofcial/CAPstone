namespace AttendanceSystemAPI.Models
{
    public class LoginRequest
    {
        public string TeacherNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
