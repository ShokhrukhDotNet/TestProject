using Domain.Entities.Models;
using Repository.Users;

namespace Service.Users;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<IQueryable<User>> GetAllAsync()
    {
        return await userRepository.GetAllAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await userRepository.GetByIdAsync(id);
    }

    public async Task<int> CreateAsync(User user)
    {
        await userRepository.CreateAsync(user);
        return user.Id;
    }

    public async Task UpdateAsync(User user)
    {
        await userRepository.UpdateAsync(user);
    }

    public async Task DeleteAsync(int id)
    {
        var user = await userRepository.GetByIdAsync(id);
        await userRepository.DeleteAsync(user);
    }
}
