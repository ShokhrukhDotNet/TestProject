using Domain.Entities.Models;

namespace Service.Users;

public interface IUserService
{
    Task<IQueryable<User>> GetAllAsync();
    Task<User> GetByIdAsync(int id);
    Task<int> CreateAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
}
