using College.Domain.Abstractions;
using College.Domain.Constants;
using College.Domain.Exceptions;

namespace College.Domain.Entities;

public class Course : BaseEntity
{
    public string Title { get; private set; }
    public int WorkloadHours { get; private set; }
    public ICollection<Enrollment> Enrollments { get; private set; } = new List<Enrollment>();

    public Course(string title, int workloadHours)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException(BusinessMessages.CourseTitleRequired);

        if (workloadHours < CourseConstants.MinWorkloadHours || workloadHours > CourseConstants.MaxWorkloadHours)
            throw new DomainException(BusinessMessages.InvalidWorkload);

        Title = title;
        WorkloadHours = workloadHours;
    }

    public ICollection<Student> Students { get; private set; } = new List<Student>();
}
