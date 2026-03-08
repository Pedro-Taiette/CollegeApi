using College.Domain.Entities;

namespace College.Domain.Abstractions;

public interface IEnrollmentRepository
{
    Task<Enrollment?> GetAsync(Guid studentId, Guid courseId);
    Task<IEnumerable<Enrollment>> GetByStudentIdAsync(Guid studentId);
    Task<IEnumerable<Enrollment>> GetByCourseIdAsync(Guid courseId);
    Task AddAsync(Enrollment enrollment);
    Task RemoveAsync(Enrollment enrollment);
}
