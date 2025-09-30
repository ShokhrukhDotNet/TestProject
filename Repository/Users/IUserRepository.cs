using Domain.Entities.Models;

namespace Repository.Users;

public interface IUserRepository
{
    Task<IQueryable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task CreateAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
    Task<List<User>> GetByIdsAsync(List<int> ids);
}
