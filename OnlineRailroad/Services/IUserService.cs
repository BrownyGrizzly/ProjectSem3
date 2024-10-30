using OnlineRailroad.DTOs;
using OnlineRailroad.Models;

namespace OnlineRailroad.Services
{
    public interface IUserService
    {
        Task<User> RegisterUser(RegisterRequest registerRequest);
        Task<string?> AuthenticateUser(string identifier, string password);
        Task<User?> GetUserById(int userId);
        Task<User?> UpdateUser(int userId, User updatedUser);
    }
}
