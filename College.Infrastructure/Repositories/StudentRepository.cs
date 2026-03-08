using College.Domain.Abstractions;
using College.Domain.Entities;
using College.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace College.Infrastructure.Repositories;

public class StudentRepository: IStudentRepository
{
    private readonly CollegeDbContext _storage; // DB
    public StudentRepository(CollegeDbContext context) => _storage = context;

    public async Task AddAsync(Student student) => await _storage.Students.AddAsync(student);
    public async Task<Student?> GetByEmailAsync(string email)
        => await _storage.Students.FirstOrDefaultAsync(x => x.Email == email);
    public async Task<IEnumerable<Student>> GetAllAsync()
        => await _storage.Students.ToListAsync();
    public async Task<Student?> GetByIdAsync(Guid id)
        => await _storage.Students.FindAsync(id);
}
