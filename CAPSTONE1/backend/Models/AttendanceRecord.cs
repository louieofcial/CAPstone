namespace AttendanceSystemAPI.Models
{
    public class AttendanceRecord
    {
        public int Id { get; set; }
        public string StudentNumber { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public DateTime TimeRecorded { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}