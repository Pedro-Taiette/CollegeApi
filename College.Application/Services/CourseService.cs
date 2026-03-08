using College.Application.Abstractions;
using College.Application.Exceptions;
using College.Application.ViewModels;
using College.Domain.Abstractions;
using College.Domain.Constants;
using College.Domain.Entities;

namespace College.Application.Services;

public class CourseService: ICourseService
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

    public async Task<IEnumerable<CourseViewModel>> GetAllAsync()
    {
        var courses = await _unitOfWork.Courses.GetAllAsync();
        return courses.Select(c => new CourseViewModel(c.Id, c.Title, c.WorkloadHours));
    }

    public async Task DeleteAsync(Guid id)
    {
        var course = await _unitOfWork.Courses.GetByIdAsync(id) 
            ?? throw new BusinessException(BusinessMessages.CourseNotFound);
        
        await _unitOfWork.Courses.DeleteAsync(id);
        await _unitOfWork.CommitAsync();
    }
}