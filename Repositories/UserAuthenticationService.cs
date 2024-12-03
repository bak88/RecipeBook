using FinalСertificationRecipeBook.Models;
using System.Security.Cryptography;
using System.Text;

namespace FinalСertificationRecipeBook.Repositories
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IDictionary<string, User> _users = new Dictionary<string, User>
        {
            { "admin", new User { Name = "admin", Password = ComputeHash("admin"), Role = UserRole.Admin } },
            { "user", new User { Name = "user", Password = ComputeHash("user"), Role = UserRole.User } }
        };

        public User Authenticate(LoginModel login)
        {
            if (_users.TryGetValue(login.Name, out var user) && VerifyPassword(login.Password, user.Password))
                return user;

            return null;
        }

        private static string ComputeHash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return Convert.ToBase64String(bytes);
            }
        }

        private static bool VerifyPassword(string input, string hash)
        {
            return ComputeHash(input) == hash;
        }
    }

}
