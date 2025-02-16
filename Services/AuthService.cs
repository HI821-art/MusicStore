using MusicStore.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
namespace MusicStore.Services
{
    public class AuthService : IAuthService
    {
        private readonly MusicStoreDbContext _context;
        private readonly ILogger<AuthService> _logger;

        public AuthService(MusicStoreDbContext context, ILogger<AuthService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<User> RegisterAsync(string username, string password)
        {
            try
            {
                if (await _context.Users.AnyAsync(u => u.Username == username))
                {
                    _logger.LogWarning("Username is already taken.");
                    throw new ArgumentException("Username is already taken.");
                }

                var user = new User
                {
                    Username = username,
                    Password = password // You should hash passwords, this is just an example
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"User {username} registered successfully.");
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user.");
                throw new Exception("Error registering user.", ex);
            }
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == username && u.Password == password); // Password check logic should be hashed

                if (user == null)
                {
                    _logger.LogWarning("Invalid login attempt.");
                    throw new UnauthorizedAccessException("Invalid username or password.");
                }

                _logger.LogInformation($"User {username} logged in successfully.");
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error logging in user.");
                throw new Exception("Error logging in user.", ex);
            }
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user.");
                throw new Exception("Error fetching user.", ex);
            }
        }

        public async Task<bool> ValidatePasswordAsync(string password)
        {
            // Implement password validation logic
            return !string.IsNullOrEmpty(password) && password.Length >= 6; // Example password validation
        }

        public bool Register(string username, string password, string email)
        {
            throw new NotImplementedException();
        }

        public bool Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(string username)
        {
            throw new NotImplementedException();
        }
    }
}
