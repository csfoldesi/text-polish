using Application.Common.DTOs;
using Application.Common.Interfaces;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<User> _userManager;

    public IdentityService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<User?> CreateUserAsync(CreateUserRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.Id);
        if (user == null)
        {
            user = new User
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email,
            };
            await _userManager.CreateAsync(user);
        }
        return user;
    }

    public async Task<User?> UpdateUserAsync(CreateUserRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.Id);
        if (user != null)
        {
            if (request.Email != null)
            {
                user.Email = request.Email;
            }
            if (request.FirstName != null)
            {
                user.FirstName = request.FirstName;
            }
            if (request.LastName != null)
            {
                user.LastName = request.LastName;
            }
            await _userManager.UpdateAsync(user);
        }
        return user;
    }

    public async Task<User?> DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            await _userManager.DeleteAsync(user);
        }
        return user;
    }
}
