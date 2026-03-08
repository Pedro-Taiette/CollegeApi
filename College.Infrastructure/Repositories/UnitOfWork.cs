using College.Domain.Abstractions;
using College.Infrastructure.Data;

namespace College.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly CollegeDbContext _context;
    public IStudentRepository Students { get; }
    public ICourseRepository Courses { get; }
    public IEnrollmentRepository Enrollments { get; }

    public UnitOfWork(
        CollegeDbContext context,
        IStudentRepository students,
        ICourseRepository courses,
        IEnrollmentRepository enrollments)
    {
        _context = context;
        Students = students;
        Courses = courses;
        Enrollments = enrollments;
    }

    public async Task<bool> CommitAsync() => await _context.SaveChangesAsync() > 0;
    public void Dispose() => _context.Dispose();
}
