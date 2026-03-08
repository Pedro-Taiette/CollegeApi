using College.Application.Exceptions;
using College.Domain.Abstractions;
using College.Domain.Constants;
using College.Domain.Entities;
using College.Domain.Exceptions;

namespace College.Application.Services;

public class EnrollmentService
{
    private readonly IUnitOfWork _unitOfWork;

    public EnrollmentService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task EnrollStudentAsync(Guid studentId, Guid courseId)
    {
        var student = await _unitOfWork.Students.GetByIdAsync(studentId)
            ?? throw new BusinessException(BusinessMessages.StudentNotFound);

        var course = await _unitOfWork.Courses.GetByIdAsync(courseId)
            ?? throw new BusinessException(BusinessMessages.CourseNotFound);

        var existingEnrollment = await _unitOfWork.Enrollments.GetAsync(studentId, courseId);
        if (existingEnrollment != null)
            throw new BusinessException(BusinessMessages.EnrollmentDuplicate);

        int currentTotal = student.Enrollments.Sum(e => e.Course.WorkloadHours);

        if (currentTotal + course.WorkloadHours > CourseConstants.MaxTotalSemesterWorkload)
            throw new DomainException(BusinessMessages.EnrollmentLimitExceeded);

        var enrollment = new Enrollment(studentId, courseId);
        await _unitOfWork.Enrollments.AddAsync(enrollment);
        await _unitOfWork.CommitAsync();
    }

    public async Task UnenrollStudentAsync(Guid studentId, Guid courseId)
    {
        var enrollment = await _unitOfWork.Enrollments.GetAsync(studentId, courseId)
            ?? throw new BusinessException(BusinessMessages.EnrollmentNotFound);

        await _unitOfWork.Enrollments.RemoveAsync(enrollment);
        await _unitOfWork.CommitAsync();
    }
}
