using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineRailroad.Data;
using OnlineRailroad.DTOs;
using OnlineRailroad.Enums;
using OnlineRailroad.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineRailroad.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;

        public UserService(AppDbContext context, IPasswordHasher<User> passwordHasher, IConfiguration configuration)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        // Register a new user with ROLE_USER as the default role
        public async Task<User> RegisterUser(RegisterRequest registerRequest)
        {
            if (await _context.Users.AnyAsync(u => u.Email == registerRequest.Email))
            {
                throw new ArgumentException("Email is already in use.");
            }

            var newUser = new User
            {
                UserName = registerRequest.UserName,
                Email = registerRequest.Email,
                PasswordHash = _passwordHasher.HashPassword(null, registerRequest.Password),
                UserType = UserType.ROLE_USER, // Default user role
                RegisteredDate = DateTime.UtcNow
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        // Authenticate user and return JWT token on success
        public async Task<string?> AuthenticateUser(string identifier, string password)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Email == identifier || u.UserName == identifier);

            if (user == null) return null;

            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (verificationResult != PasswordVerificationResult.Success) return null;

            // Generate JWT token
            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserID.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(ClaimTypes.Role, user.UserType.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Retrieve user by ID with null check
        public async Task<User?> GetUserById(int userId)
        {
            return await _context.Users.FindAsync(userId) ?? throw new KeyNotFoundException("User not found.");
        }

        // Update user's profile with validation
        public async Task<User?> UpdateUser(int userId, User updatedUser)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return null;

            user.FullName = updatedUser.FullName ?? user.FullName;
            user.PhoneNumber = updatedUser.PhoneNumber ?? user.PhoneNumber;
            user.Address = updatedUser.Address ?? user.Address;
            user.ProfilePicture = updatedUser.ProfilePicture ?? user.ProfilePicture;
            user.DateOfBirth = updatedUser.DateOfBirth ?? user.DateOfBirth;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
