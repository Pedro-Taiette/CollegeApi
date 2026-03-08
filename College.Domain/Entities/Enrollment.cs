using College.Domain.Abstractions;
using College.Domain.Constants;
using College.Domain.Exceptions;

namespace College.Domain.Entities;

public class Enrollment : BaseEntity
{
    public Guid StudentId { get; private set; }
    public Guid CourseId { get; private set; }

    public Student Student { get; private set; } = null!;
    public Course Course { get; private set; } = null!;

    private Enrollment() { }

    public Enrollment(Guid studentId, Guid courseId)
    {
        if (studentId == Guid.Empty)
            throw new DomainException(BusinessMessages.InvalidStudentId);

        if (courseId == Guid.Empty)
            throw new DomainException(BusinessMessages.InvalidCourseId);

        StudentId = studentId;
        CourseId = courseId;
    }
}
