using College.Application.DTOs;
using College.Domain.Abstractions;
using College.Domain.Constants;
using College.Domain.Entities;
using College.Domain.Exceptions;

namespace College.Application.Services;

public class StudentService
{
    private readonly IUnitOfWork _unitOfWork;

    public StudentService(IUnitOfWork UnitOfWork)
    {
        _unitOfWork = UnitOfWork;
    }

    private void ValidateStudentData(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException(BusinessMessages.StudentNameRequired);

        if (name.Length > 50)
            throw new DomainException(BusinessMessages.StudentNameTooLong);

        if (!email.EndsWith("@faculdade.edu", StringComparison.OrdinalIgnoreCase))
            throw new DomainException(BusinessMessages.InvalidEmailDomain);
    }

    /// <summary>
    /// Enrolls a new student after validating business rules.
    /// </summary>
    public async Task EnrollAsync(RegisterStudentRequest request)
    {
        // 1. Business Rule Validations (Fail Fast)
        ValidateStudentData(request.FirstName, request.Email);

        // 2. Uniqueness Check
        var existingStudent = await _unitOfWork.Students.GetByEmailAsync(request.Email);
        if (existingStudent != null)
            throw new DomainException(BusinessMessages.DuplicateEmail);

        // 3. Entity Creation
        var student = new Student(request.FirstName, request.Email);

        // 4. Persistence via UoW
        await _unitOfWork.Students.AddAsync(student);
        await _unitOfWork.CommitAsync();
    }

    /// <summary>
    /// Retrieves all active students.
    /// </summary>
    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        return await _unitOfWork.Students.GetAllAsync();
    }

    /// <summary>
    /// Performs a soft delete by deactivating the student record.
    /// </summary>
    public async Task DeactivateAsync(Guid id)
    {
        var student = await _unitOfWork.Students.GetByIdAsync(id);

        if (student == null)
            throw new DomainException(BusinessMessages.StudentNotFound);

        student.Deactivate();
        await _unitOfWork.CommitAsync();
    }
}