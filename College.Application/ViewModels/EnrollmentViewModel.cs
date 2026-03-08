namespace College.Application.ViewModels;

public record EnrollmentViewModel(Guid Id, Guid StudentId, Guid CourseId, DateTime CreatedAt);
