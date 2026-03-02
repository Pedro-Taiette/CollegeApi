using College.Domain.Abstractions;
using College.Domain.Entities;

namespace College.Infrastructure.Repositories;

public class StudentRepository: IStudentRepository
{
    private readonly List<Student> _storage = new(); // DB
    
    public async Task AddAsync(Student student)
    {
        _storage.Add(student);
        await Task.CompletedTask;
    }

    public async Task <Student?> GetByIdAsync(Guid id)
    {
        var student = _storage.FirstOrDefault(s => s.Id == id);
        return await Task.FromResult(student);
    }

    public async Task <Student?> GetByEmailAsync(string email)
    {
        var student = _storage.FirstOrDefault(s => s.Email == email);
        return await Task.FromResult(student);
    }   

    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        return _storage;
    }
}
