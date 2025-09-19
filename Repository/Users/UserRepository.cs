using Domain.Entities;
using Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Users;

public class UserRepository(DataContext context) : IUserRepository
{
    public async Task<IQueryable<User>> GetAllAsync()
    {
        return await Task.FromResult(context.Users);
    }

    public Task<User?> GetByIdAsync(int id)
    {
        return context.Users.FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task CreateAsync(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        context.Users.Remove(user);
        await context.SaveChangesAsync();
    }
}
