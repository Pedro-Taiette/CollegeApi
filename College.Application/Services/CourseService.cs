using College.Application.ViewModels;
using College.Domain.Abstractions;
using College.Domain.Entities;

namespace College.Application.Services;

public class CourseService
{
    private readonly IUnitOfWork _unitOfWork;

    public CourseService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

  
    public async Task<CourseViewModel> CreateAsync(CreateCourseViewModel viewModel)
    {
        var course = new Course(viewModel.Title, viewModel.WorkloadHours);

        await _unitOfWork.Courses.AddAsync(course);
        await _unitOfWork.CommitAsync();

        return new CourseViewModel(course.Id, course.Title, course.WorkloadHours);
    }
}