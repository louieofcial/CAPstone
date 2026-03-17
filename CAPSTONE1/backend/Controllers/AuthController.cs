using AttendanceSystemAPI.Data;
using AttendanceSystemAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("teacher-login")]
        public async Task<IActionResult> TeacherLogin([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.TeacherNumber) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new { message = "Teacher number and password are required." });
            }

            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(t =>
                    t.TeacherNumber == request.TeacherNumber &&
                    t.Password == request.Password);

            if (teacher == null)
            {
                return Unauthorized(new { message = "Invalid teacher number or password." });
            }

            return Ok(new
            {
                message = "Login successful.",
                teacher = new
                {
                    teacher.Id,
                    teacher.TeacherNumber,
                    teacher.FirstName,
                    teacher.MiddleName,
                    teacher.LastName
                }
            });
        }
    }
}