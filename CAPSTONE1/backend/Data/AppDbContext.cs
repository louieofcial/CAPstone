using AttendanceSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystemAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<AttendanceRecord> AttendanceRecords => Set<AttendanceRecord>();
    }
}