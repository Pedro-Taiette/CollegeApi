using College.Domain.Entities;

namespace College.Domain.Abstractions;

public interface ICourseRepository
{
    Task<Course?> GetByIdAsync(Guid id);
    Task AddAsync(Course course);
    Task<IEnumerable<Course>> GetAllAsync();
    Task DeleteAsync(Guid id);
}