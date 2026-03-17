using AttendanceSystemAPI.Data;
using AttendanceSystemAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AttendanceController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAttendance()
        {
            var records = await _context.AttendanceRecords
                .OrderByDescending(a => a.TimeRecorded)
                .ToListAsync();

            return Ok(records);
        }

        [HttpPost("mark")]
        public async Task<IActionResult> MarkAttendance([FromBody] MarkAttendanceRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.StudentNumber))
            {
                return BadRequest(new { message = "Student number is required." });
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.StudentNumber == request.StudentNumber);

            if (student == null)
            {
                return NotFound(new { message = "Student not found." });
            }

            var now = DateTime.Now;
            var status = now.Hour <= 8 ? "Present" : "Late";

            var record = new AttendanceRecord
            {
                StudentNumber = student.StudentNumber,
                StudentName = $"{student.FirstName} {student.LastName}",
                TimeRecorded = now,
                Status = status
            };

            _context.AttendanceRecords.Add(record);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Attendance marked successfully.",
                record
            });
        }
    }
}