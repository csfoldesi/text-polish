using Application.Common.DTOs;
using Domain;

namespace Application.Common.Interfaces;

public interface IIdentityService
{
    Task<User?> CreateUserAsync(CreateUserRequest request);
    Task<User?> UpdateUserAsync(CreateUserRequest request);
    Task<User?> DeleteUserAsync(string userId);
}
