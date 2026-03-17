using AttendanceSystemAPI.Data;
using AttendanceSystemAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeachersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TeachersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTeachers()
        {
            var teachers = await _context.Teachers
                .OrderBy(t => t.LastName)
                .ThenBy(t => t.FirstName)
                .ToListAsync();

            return Ok(teachers);
        }

        [HttpPost]
        public async Task<IActionResult> AddTeacher([FromBody] Teacher teacher)
        {
            if (string.IsNullOrWhiteSpace(teacher.TeacherNumber) ||
                string.IsNullOrWhiteSpace(teacher.FirstName) ||
                string.IsNullOrWhiteSpace(teacher.LastName) ||
                string.IsNullOrWhiteSpace(teacher.Password))
            {
                return BadRequest(new { message = "Teacher number, first name, last name, and password are required." });
            }

            var exists = await _context.Teachers
                .AnyAsync(t => t.TeacherNumber == teacher.TeacherNumber);

            if (exists)
            {
                return Conflict(new { message = "Teacher number already exists." });
            }

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Teacher added successfully." });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null)
            {
                return NotFound(new { message = "Teacher not found." });
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Teacher deleted successfully." });
        }
    }
}