using College.Domain.Entities;

namespace College.Domain.Abstractions;

public interface IStudentRepository
{
    Task<Student?> GetByEmailAsync(string email);
    Task<IEnumerable<Student>> GetAllAsync();
    Task AddAsync(Student student);
}
