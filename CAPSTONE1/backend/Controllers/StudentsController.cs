using AttendanceSystemAPI.Data;
using AttendanceSystemAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _context.Students
                .OrderBy(s => s.LastName)
                .ThenBy(s => s.FirstName)
                .ToListAsync();

            return Ok(students);
        }

        [HttpGet("{studentNumber}")]
        public async Task<IActionResult> GetStudent(string studentNumber)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.StudentNumber == studentNumber);

            if (student == null)
            {
                return NotFound(new { message = "Student not found." });
            }

            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            if (string.IsNullOrWhiteSpace(student.StudentNumber) ||
                string.IsNullOrWhiteSpace(student.FirstName) ||
                string.IsNullOrWhiteSpace(student.LastName))
            {
                return BadRequest(new { message = "Student number, first name, and last name are required." });
            }

            var exists = await _context.Students
                .AnyAsync(s => s.StudentNumber == student.StudentNumber);

            if (exists)
            {
                return Conflict(new { message = "Student number already exists." });
            }

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Student added successfully." });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound(new { message = "Student not found." });
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Student deleted successfully." });
        }
    }
}
