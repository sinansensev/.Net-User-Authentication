// IUserService.cs

using System.Linq;
using System.Threading.Tasks;
using PayosferIdentity.Models;

namespace PayosferIdentity.Services
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(User user);
        Task<User> LoginAsync(string email, string password);
        Task<User> GetUserByEmailAsync(string email);
        Task<IQueryable<User>> GetAllUsersAsync(); 
    }
}
