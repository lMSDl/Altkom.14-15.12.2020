using Models;

namespace WebApi.Services
{
    public interface IAuthService
    {
        string Authenticate(User user);
    }
}