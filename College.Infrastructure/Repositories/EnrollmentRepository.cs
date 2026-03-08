using College.Domain.Abstractions;
using College.Domain.Entities;
using College.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace College.Infrastructure.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly CollegeDbContext _context;

    public EnrollmentRepository(CollegeDbContext context) => _context = context;

    public async Task<Enrollment?> GetAsync(Guid studentId, Guid courseId)
    {
        return await _context.Enrollments
            .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId);
    }

    public async Task<IEnumerable<Enrollment>> GetByStudentIdAsync(Guid studentId)
    {
        return await _context.Enrollments
            .Where(e => e.StudentId == studentId)
            .Include(e => e.Course)
            .ToListAsync();
    }

    public async Task<IEnumerable<Enrollment>> GetByCourseIdAsync(Guid courseId)
    {
        return await _context.Enrollments
            .Where(e => e.CourseId == courseId)
            .Include(e => e.Student)
            .ToListAsync();
    }

    public async Task AddAsync(Enrollment enrollment)
    {
        await _context.Enrollments.AddAsync(enrollment);
    }

    public async Task RemoveAsync(Enrollment enrollment)
    {
        _context.Enrollments.Remove(enrollment);
    }
}
