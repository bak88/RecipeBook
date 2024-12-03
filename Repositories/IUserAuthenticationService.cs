using FinalСertificationRecipeBook.Models;

namespace FinalСertificationRecipeBook.Repositories
{
    public interface IUserAuthenticationService
    {
        User Authenticate(LoginModel login);
    }
}
