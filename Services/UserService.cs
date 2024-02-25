// UserService.cs

using Microsoft.EntityFrameworkCore;
using PayosferIdentity.Data;
using PayosferIdentity.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PayosferIdentity.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> LoginAsync(string email, string password)
        {
            var passwordHash = HashPassword(password);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == passwordHash);
            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IQueryable<User>> GetAllUsersAsync()
        {
            return await Task.FromResult(_context.Users.AsQueryable());
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
