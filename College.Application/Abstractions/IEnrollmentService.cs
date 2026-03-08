using College.Application.ViewModels;

namespace College.Application.Abstractions;

public interface IEnrollmentService
{
    Task EnrollStudentAsync(Guid studentId, Guid courseId);
    Task UnenrollAsync(Guid enrollmentId);
    Task<IEnumerable<EnrollmentViewModel>> GetByStudentAsync(Guid studentId);
    Task<IEnumerable<EnrollmentViewModel>> GetByCourseAsync(Guid courseId);
}