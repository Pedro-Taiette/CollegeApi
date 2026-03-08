using College.Application.ViewModels;
using College.Application.Exceptions;
using College.Domain.Abstractions;
using College.Domain.Constants;
using College.Domain.Entities;

namespace College.Application.Services;

public class StudentService
{
    private readonly IUnitOfWork _unitOfWork;

    public StudentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task EnrollAsync(CreateStudentViewModel viewModel)
    {
        var student = new Student(viewModel.FirstName, viewModel.Email);

        var existingStudent = await _unitOfWork.Students.GetByEmailAsync(viewModel.Email);
        if (existingStudent != null)
            throw new BusinessException(BusinessMessages.DuplicateEmail);

        await _unitOfWork.Students.AddAsync(student);
        await _unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<StudentViewModel>> GetAllAsync()
    {
        var students = await _unitOfWork.Students.GetAllAsync();
        return students.Select(s => new StudentViewModel(s.Id, s.FirstName, s.Email));
    }

    public async Task DeactivateAsync(Guid id)
    {
        var student = await _unitOfWork.Students.GetByIdAsync(id);
        if (student == null)
            throw new BusinessException(BusinessMessages.StudentNotFound);

        student.Deactivate();
        await _unitOfWork.CommitAsync();
    }
}