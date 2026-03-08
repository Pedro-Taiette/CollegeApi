using College.Application.ViewModels;

namespace College.Application.Abstractions;

public interface ICourseService
{
    Task<CourseViewModel> CreateAsync(CreateCourseViewModel viewModel);
    Task<IEnumerable<CourseViewModel>> GetAllAsync();
    Task DeleteAsync(Guid id);
}