using Api.ViewResponse;
using Domain.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Users;

namespace Api.Controllers;

[Route("api/users")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<ApiResponse> CreateAsync([FromBody] User user)
    {
        var userId = await userService.CreateAsync(user);
        return ApiResponse.Success(userId);
    }

    [HttpGet]
    public async Task<ApiResponse> GetAllAsync()
    {
        var users = await userService.GetAllAsync();
        return ApiResponse.Success(users);
    }

    [HttpGet("{id}")]
    public async Task<ApiResponse> GetByIdAsync([FromRoute] int id)
    {
        var user = await userService.GetByIdAsync(id);
        return ApiResponse.Success(user);
    }

    [HttpPut]
    public async Task<ApiResponse> UpdateAsync(User user)
    {
        await userService.UpdateAsync(user);
        return ApiResponse.Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse> DeleteAsync([FromRoute] int id)
    {
        await userService.DeleteAsync(id);
        return ApiResponse.Ok();
    }
}
